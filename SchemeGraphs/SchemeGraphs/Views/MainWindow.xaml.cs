using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;
using SchemeGraphs.Graph;
using SchemeGraphs.Graph.Implementation;
using SchemeGraphs.Model;
using SchemeGraphs.ViewModels;
using SchemeLibrary.Loaders;
using SchemeLibrary.Loaders.Implementation;
using SchemeLibrary.Math.Implementation;

namespace SchemeGraphs.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel Graph { get; private set; }
        public ObservableLineSeriesViewModelCollection ModelViewCollection { get; set; }
        public LineSeriesViewModel CurrentModel { get; set; }

        private ObservableLineSeriesModelCollection modelCollection;
        
        private ISchemeLoader loader;
        private ILineSeriesTranformer transformer;
        private IChart chart;

        public MainWindow()
        {
            DataContext = this;

            Graph = new PlotModel();
            chart = new ChartAdapter(Graph);
            chart.SetLinearScale(AxisProperty.Both);

            loader = new SchemeLoader();
            loader.Import(@"Scheme.rkt");

            transformer = new LineSeriesTransformer(new FunctionPlotter(new ProxySchemeEvaluator()));

            modelCollection = new ObservableLineSeriesModelCollection();
            modelCollection.CollectionChanged += ModelChangedEvent;

            ModelViewCollection = new ObservableLineSeriesViewModelCollection();
            ModelViewCollection.CollectionChanged += ModelViewCollectionOnCollectionChanged;
            ModelViewCollection.AddModel(new LineSeriesViewModel
                                      {
                                          Function = "(lambda (x) x)",
                                          Name = "Example 1",
                                      });
            InitializeComponent();
            ToggleEnabled(false);
        }

        private void ModelViewCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
        }

        private void ModelChangedEvent(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            chart.Clear();
            foreach (var model in modelCollection)
            {
                chart.AddLineSeries(model.Name, model.FunctionPlots);
                if (model.HasDerivative)
                {
                    chart.AddLineSeries(model.Name + Constants.DerivativeNamePostfix, model.DerivativePlots);
                }
            }
        }

        private void AddLineSeries(object sender, RoutedEventArgs e)
        {
            try
            {
                ModelViewCollection.AddModel(new LineSeriesViewModel
                                     {
                                         Name = string.Format("New Func ({0})",ModelViewCollection.Count(x => x.Name.StartsWith("New Func ("))+1),
                                     });
            }
            catch (Exception ex)
            {
                tb_output.AppendText(ex.Message);
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            modelCollection.Clear();
            ModelViewCollection.ClearModel();
            tb_output.Text = string.Empty;
        }

        private void CbLinearChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null) 
                chart.SetLinearScale(AxisProperty.Both);
        }

        private void CbLogChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null) 
                chart.SetLogrithmicScale(AxisProperty.Both);
        }

        #region because binding doent cut it

        private static int test = 0;

        private void SaveCurrentModel(object sender, RoutedEventArgs e)
        {
            if (CurrentModel == null) return;
            CurrentModel.Function = tb_function.Text;
            CurrentModel.Name = tb_name.Text;
            CurrentModel.XFrom = tb_xfrom.Text;
            CurrentModel.XTo = tb_xto.Text;
            CurrentModel.Dx = tb_dx.Text;
            CurrentModel.Samples = tb_samples.Text;
            CurrentModel.HasDerivative = (bool) cb_derivative.IsChecked ;
            CurrentModel.HasIntegral = (bool) cb_integral.IsChecked;
            try
            {
                var model = transformer.Transform(CurrentModel);
                var existing = modelCollection.FirstOrDefault(x => x.Name == model.Name);
                modelCollection.Remove(existing);
                    modelCollection.Add(model);
            }
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleEnabled(CurrentModel != null);
            if (CurrentModel == null) return;
            tb_function.Text = CurrentModel.Function;
            tb_name.Text = CurrentModel.Name;
            tb_xfrom.Text = CurrentModel.XFrom;
            tb_xto.Text = CurrentModel.XTo;
            tb_dx.Text = CurrentModel.Dx;
            tb_samples.Text = CurrentModel.Samples;
            cb_derivative.IsChecked = CurrentModel.HasDerivative;
            cb_integral.IsChecked = CurrentModel.HasIntegral;
        }

        private void ToggleEnabled(bool flag)
        {
            tb_function.IsEnabled = flag;
            tb_name.IsEnabled = flag;
            tb_xfrom.IsEnabled = flag;
            tb_xto.IsEnabled = flag;
            tb_dx.IsEnabled = flag;
            tb_samples.IsEnabled = flag;
            cb_derivative.IsEnabled = flag;
            cb_integral.IsEnabled = flag;
            bt_SaveCurrentModel.IsEnabled = flag;
        }

        #endregion
    }
}
