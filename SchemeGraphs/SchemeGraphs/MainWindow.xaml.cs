using System;
using System.Collections.Generic;
using System.Windows;
using SchemeLibrary.Loaders.Implementation;


namespace SchemeGraphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProxySchemeEvaluator _proxySchemeHandler;
        public MainWindow()
        {
            InitializeComponent();
            _proxySchemeHandler = new ProxySchemeEvaluator();
            showColumnChart();
        }

        private void Evaluate_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Major changes - Can only work with integers, not symbols
            DisplayArea.Text = _proxySchemeHandler.Evaluate<Int32>(Input.Text).ToString();
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
