using System;
using System.Windows;
using OxyPlot;
using SchemeGraphs.Graph;
using SchemeGraphs.Graph.Implementation;
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
        public PlotModel MyModel { get; private set; }
        
        private ISchemeLoader loader;
        private IFunctionPlotter plotter;
        private IChart chart;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            MyModel = new PlotModel();

            loader = new SchemeLoader();
            loader.Import(@"Scheme.rkt");

            plotter = new FunctionPlotter(new ProxySchemeEvaluator());
            chart = new ChartAdapter(MyModel);

            chart.SetLinearScale(AxisProperty.Both);
        }

        private void EvaluateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var xBegin = Double.Parse(tb_xfrom.Text);
                var xEnd = Double.Parse(tb_xto.Text);
                var samples = Int32.Parse(tb_samples.Text);
                var plots = plotter.PlotFunction(tb_function.Text, xBegin, xEnd, samples);

                chart.AddLineSeries("Example 1",plots);
            }
            catch (Exception ex)
            {
                tb_output.AppendText(ex.Message);
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            chart.Clear();
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
    }
}
