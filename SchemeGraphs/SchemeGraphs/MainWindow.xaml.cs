using System;
using System.Collections.Generic;
using System.Windows;
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
        private ISchemeEvaluator evaluator;
        private ISchemeLoader loader;
        private IFunctionPlotter plotter;
        public MainWindow()
        {
            InitializeComponent();

            loader = new SchemeLoader();
            loader.Import("Scheme.rkt");

            evaluator = new ProxySchemeEvaluator();
            plotter = new FunctionPlotter(evaluator);
            
            showColumnChart();
        }

        private void Evaluate_Click(object sender, RoutedEventArgs e)
        {
            ////TODO: Major changes - Can only work with integers, not symbols
            //DisplayArea.Text = evaluator.Evaluate<Int32>(Input.Text).ToString();
            var plots = plotter.PlotFunction(Input.Text, 0, 5, 10);
            lineChart.DataContext = plots;
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
            lineChart.DataContext = valueList;
        }
    }
}
