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
        public LineSeriesViewModel CurrentModel { get; set; }

        private ObservableLineSeriesModelCollection modelCollection;

        private ISchemeLoader loader;
        private ILineSeriesTranformer transformer;
        private ICalculate calculator;
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
            transformer = new LineSeriesTransformer(schemeCalculator);
            this.calculator = schemeCalculator;

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
                if (model.HasIntegral)
                {
                    chart.AddIntegralBoxes(model.Name+Constants.IntegralNamePostfix, model.IntegralPlots, model.IntegralValue);
                }
            }
        }

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
                tb_output.AppendText(ex.Message);
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            modelCollection.Clear();
            ModelViewCollection.ClearModel();
            chart.Clear();
            tb_output.Text = string.Empty;
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
            if(chart != null)
                chart.SetLogrithmicScale(AxisProperty.Y);
        }
        
        private void CbXLogChecked(object sender, RoutedEventArgs e)
        {
            if (chart != null)
                chart.SetLogrithmicScale(AxisProperty.X);
        }
        #endregion

        private void DrawCurrentModel(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = transformer.Transform(CurrentModel);
                var existing = modelCollection.FirstOrDefault(x => x.Uid == model.Uid);
                modelCollection.Remove(existing);
                modelCollection.Add(model);
            }
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
            }
        }

        private void Bt_CalcInt_OnClick(object sender, RoutedEventArgs e)
        {
            this.CurrentModel.Integral = this.calculator.Integrate(this.CurrentModel.Function,
                double.Parse(this.CurrentModel.XFrom), double.Parse(this.CurrentModel.XTo),
                Int32.Parse(this.CurrentModel.Rectangles)).ToString();
        }

        private void DeleteSelectedLineSeries(object sender, RoutedEventArgs e)
        {
            this.modelCollection.Remove(modelCollection.FirstOrDefault(x => x.Uid == this.CurrentModel.Uid));
            this.ModelViewCollection.RemoveModel(this.CurrentModel);
        }


    }
}
