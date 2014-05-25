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
using SchemeLibrary.Math;
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
        private ObservableLineSeriesModelCollection modelCollection;
        public LineSeriesViewModel CurrentModel { get; set; }

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

            var evaluator = new ProxySchemeEvaluator();
            var schemeCalculator = new SchemeCalculator(evaluator);
            transformer = new LineSeriesTransformer((IFunctionPlotter)schemeCalculator,(ICalculate)schemeCalculator);

            modelCollection = new ObservableLineSeriesModelCollection();
            modelCollection.CollectionChanged += ModelChanged;

            ModelViewCollection = new ObservableLineSeriesViewModelCollection();
            ModelViewCollection.AddModel(new LineSeriesViewModel
                                      {
                                          Function = "(lambda (x) x)",
                                          Name = "Example 1",
                                      });
            InitializeComponent();
        }

        private void ModelChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            chart.Clear();
            foreach (var model in modelCollection)
            {
                chart.AddLineSeries(model.Name, model.FunctionPlots);
                if (model.HasDerivative)
                {
                    chart.AddLineSeries(model.Name + Constants.DerivativeNamePostfix, model.DerivativePlots);
                }
                if (model.HasIntegral)
                {
                    chart.AddIntegralBoxes(model.Name + Constants.IntegralNamePostfix, model.IntegralPlots, model.IntegralValue);
                    var viewModel = ModelViewCollection.FirstOrDefault(x => x.Uid == model.Uid);
                    if (viewModel != null)
                    {
                        viewModel.IntegralValue = model.IntegralValue.ToString();
                    }
                }
            }
        }

        #region Scale checkboxes
        private void CbLinearChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null)
                chart.SetLinearScale(AxisProperty.Both);
        }

        private void CbXYLogChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null)
                chart.SetLogrithmicScale(AxisProperty.Both);
        }

        private void CbYLogChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null)
                chart.SetLogrithmicScale(AxisProperty.Y);
        }

        private void CbXLogChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null)
                chart.SetLogrithmicScale(AxisProperty.X);
        }
        #endregion

        #region buttons

        private void AddLineSeries(object sender, RoutedEventArgs e)
        {
            try
            {
                ModelViewCollection.AddModel(new LineSeriesViewModel
                {
                    Name = string.Format("New Func ({0})", ModelViewCollection.Count(x => x.Name.StartsWith("New Func (")) + 1),
                });
            }
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
            }
        }

        private void DeleteSelectedLineSeries(object sender, RoutedEventArgs e)
        {
            if (CurrentModel != null)
            {
                this.chart.Clear();
                modelCollection.Remove(modelCollection.FirstOrDefault(x => x.Uid == CurrentModel.Uid));
                this.ModelViewCollection.RemoveModel(this.CurrentModel);
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ModelViewCollection.ClearModel();
            modelCollection.Clear();
            tb_output.Text = string.Empty;
        }

        private void DrawCurrentModel(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentModel != null)
                {
                    modelCollection.FirstOrDefault(x => x.Uid == CurrentModel.Uid);
                    modelCollection.Remove(modelCollection.FirstOrDefault(x => x.Uid == CurrentModel.Uid));
                    LineSeriesModel inserted = transformer.Transform(CurrentModel);
                    modelCollection.Add(inserted);
                    tb_output.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
            }
        }

        #endregion
    }
}
