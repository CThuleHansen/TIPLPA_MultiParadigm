using System;
using System.Collections.Generic;
using System.Windows;
using SchemeLibrary.Loaders;


namespace SchemeGraphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SchemeEvalProxy schemeHandler;
        public MainWindow()
        {
            InitializeComponent();
            schemeHandler = new SchemeEvalProxy();
            showColumnChart();
        }

        private void Evaluate_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Major changes - Can only work with integers, not symbols
            DisplayArea.Text = schemeHandler.Evaluate<Int32>(Input.Text).ToString();
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
