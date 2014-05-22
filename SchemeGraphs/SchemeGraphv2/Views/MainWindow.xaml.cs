using System;
using System.Windows;
using SchemeGraphv2.Models;
using SchemeLibrary.Loaders.Implementation;
using SchemeLibrary.Math;
using SchemeLibrary.Math.Implementation;

namespace SchemeGraphv2.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IChart chart;
        private IFunctionPlotter plotter;

        public MainWindow()
        {
            InitializeComponent();
            chart = new ChartAdapter(charter);
            var loader = new SchemeLoader();
            loader.Import("Scheme.rkt");

            plotter= new FunctionPlotter(new ProxySchemeEvaluator());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x1 = Double.Parse(tb_xBegin.Text);
                var x2 = Double.Parse(tb_xEnd.Text);
                var samples = Int32.Parse(tb_noOfPoints.Text);

                var plots = plotter.PlotFunction(tb_function.Text, x1, x2, samples);
                chart.Add(tb_function.GetHashCode().ToString().Substring(0, 4), plots); //TODO: generate a proper identifier string

            }
            catch (Exception ex)
            {
                tb_output.AppendText(ex.Message+"\n");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            chart.Clear();
        }
    }
}
