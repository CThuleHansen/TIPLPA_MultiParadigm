using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SchemeLibrary;



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
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            valueList.Add(new KeyValuePair<string, int>("1", 60));
            valueList.Add(new KeyValuePair<string, int>("2", 20));
            valueList.Add(new KeyValuePair<string, int>("3", 50));
            valueList.Add(new KeyValuePair<string, int>("4", 30));
            valueList.Add(new KeyValuePair<string, int>("5", 40));

            //Setting data for line chart
            lineChart.DataContext = valueList;
        }
    }
}
