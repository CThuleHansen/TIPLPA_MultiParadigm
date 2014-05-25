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

            ModelViewCollection = new ObservableLineSeriesViewModelCollection();
            ModelViewCollection.AddModel(new LineSeriesViewModel
                                      {
                                          Function = "(lambda (x) x)",
                                          Name = "Example 1",
                                      });
            InitializeComponent();
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
                tb_output.AppendText(ex.Message);
            }
        }

        private void DeleteSelectedLineSeries(object sender, RoutedEventArgs e)
        {
            this.chart.Clear();
            this.ModelViewCollection.RemoveModel(this.CurrentModel);
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ModelViewCollection.ClearModel();
            chart.Clear();
            tb_output.Text = string.Empty;
        }

        private void DrawCurrentModel(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = transformer.Transform(CurrentModel);
                if (model.FunctionPlots != null) this.CurrentModel.FunctionPlots = model.FunctionPlots;
                if (this.CurrentModel.HasDerivative)
                {
                    if (model.DerivativePlots != null)
                    {
                        this.CurrentModel.DerivativePlots = model.DerivativePlots;
                    }
                }
                AddLineSeriesToChart(this.CurrentModel);
                tb_output.Text = "";
            }
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
            }
        }

        private void Bt_CalcInt_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.CurrentModel.Integral = this.transformer.CalculateIntegral(this.CurrentModel).ToString();
                tb_output.Text = "";
            }
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
            }
            
        }

        #endregion

        private void LstModels_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddLineSeriesToChart(this.CurrentModel);
        }

        private void AddLineSeriesToChart(LineSeriesViewModel lineSeriesViewModel)
        {
            if (this.CurrentModel != null)
            {
                this.chart.Clear();

                if (lineSeriesViewModel.FunctionPlots != null)
                    this.chart.AddLineSeries(lineSeriesViewModel.Name, lineSeriesViewModel.FunctionPlots);
                if (lineSeriesViewModel.DerivativePlots != null && lineSeriesViewModel.HasDerivative)
                    this.chart.AddLineSeries(lineSeriesViewModel.Name + Constants.DerivativeNamePostfix,
                        lineSeriesViewModel.DerivativePlots);
            }
        }
    }
}
