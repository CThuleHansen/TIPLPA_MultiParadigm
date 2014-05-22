using System.Collections.Generic;
using System.Windows.Controls.DataVisualization.Charting;

namespace SchemeGraphv2.Models
{
    public class ChartAdapter : IChart
    {
        private readonly Chart chartBase;
        private Dictionary<string, IEnumerable<KeyValuePair<double, double>>> lineSeries;

        public ChartAdapter(Chart chartBase)
        {
            this.chartBase = chartBase;
            lineSeries = new Dictionary<string, IEnumerable<KeyValuePair<double, double>>>();
        }

        public void Add(string name, IEnumerable<KeyValuePair<double, double>> chart)
        {
            lineSeries.Add(name, chart);
            Validate();
        }

        public void Remove(string name)
        {
            lineSeries.Remove(name);
            Validate();
        }

        public void Clear()
        {
            lineSeries.Clear();
            Validate();
        }

        private void Validate()
        {
            chartBase.Series.Clear();

            foreach (var lineSerie in lineSeries)
            {
                var line = new LineSeries
                {
                    DependentValuePath = "Value",
                    IndependentValuePath = "Key",
                    ItemsSource = lineSerie.Value
                };
                chartBase.Series.Add(line);
            }
        }
    }
}
