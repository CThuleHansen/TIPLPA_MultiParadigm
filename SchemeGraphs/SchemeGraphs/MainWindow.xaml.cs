using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SchemeLibrary.Loaders;
using SchemeLibrary.Loaders.Implementation;
using SchemeLibrary.Math;
using SchemeLibrary.Math.Implementation;


namespace SchemeGraphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel MyModel { get; private set; }
        private ISchemeEvaluator evaluator;
        private ISchemeLoader loader;
        private IFunctionPlotter plotter;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            loader = new SchemeLoader();
            loader.Import(@"..\..\..\..\Scheme\Scheme.rkt");

            evaluator = new ProxySchemeEvaluator();
            plotter = new FunctionPlotter(evaluator);
            
            showColumnChart();
        }

        private void Evaluate_Click(object sender, RoutedEventArgs e)
        {
        //    ////TODO: Major changes - Can only work with integers, not symbols
        //    //DisplayArea.Text = evaluator.Evaluate<Int32>(Input.Text).ToString();
        //    var plots = plotter.PlotFunction(Input.Text, Convert.ToDouble(XFrom.Text), Convert.ToDouble(XTo.Text), Convert.ToInt16(NumberOfPoints.Text));
        //    lineChart.DataContext = plots;
            this.MyModel.Series.Clear();
            var logaxies = new LogarithmicAxis();
            //logaxies.Maximum = 100;
            this.MyModel.Axes[1] = logaxies;
            //var lineSeries = new LineSeries();
            //lineSeries.Points.Add(new DataPoint(1, 2));
            //lineSeries.Points.Add(new DataPoint(2, 4));
            //lineSeries.Points.Add(new DataPoint(3, 9));
            //lineSeries.Points.Add(new DataPoint(4, 16));
            //lineSeries.Points.Add(new DataPoint(5, 25));
            //this.MyModel.Series.Add(lineSeries);
            var funcSeries = new FunctionSeries(myFunc , 0, 10, 0.1, "log10(x)");
            this.MyModel.Series.Add(funcSeries);
            this.MyModel.InvalidatePlot(true);
        }

        private double myFunc(double x)
        {
            return Math.Pow(10, x);
        }

        private void showColumnChart()
        {
            var valueList = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("1", 60),
                new KeyValuePair<string, int>("2", 20),
                new KeyValuePair<string, int>("3", 50),
                new KeyValuePair<string, int>("4", 30),
                new KeyValuePair<string, int>("5", 40)
            };

            //Setting data for line chart
            //lineChart.DataContext = valueList;
            this.MyModel = new PlotModel("Example 1");
            //var lineSeries = new LineSeries();
            //lineSeries.Points.Add(new DataPoint(1,60));
            //lineSeries.Points.Add(new DataPoint(2, 20));
            //lineSeries.Points.Add(new DataPoint(3, 50));
            //lineSeries.Points.Add(new DataPoint(4, 30));
            //lineSeries.Points.Add(new DataPoint(5, 40));
            //this.MyModel.Series.Add(lineSeries);
            var funcSeries = new FunctionSeries(myFunc, 0, 10, 0.1, "log10(x)");
            this.MyModel.Series.Add(funcSeries);
        }
    }
}
