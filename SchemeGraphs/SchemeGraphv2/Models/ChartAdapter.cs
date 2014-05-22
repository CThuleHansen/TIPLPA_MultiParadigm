using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.DataVisualization.Charting;

namespace SchemeGraphv2.Models
{
    public class ChartAdapter : IChart
    {
        private readonly Chart chartBase;
        private Dictionary<string, IEnumerable<KeyValuePair<double, double>>> lineSeries;
        private List<Dictionary<string, IEnumerable<KeyValuePair<double, double>>>> dataSourceList;
        
        public ChartAdapter(Chart chartBase)
        {
            this.chartBase = chartBase;
            lineSeries = new Dictionary<string, IEnumerable<KeyValuePair<double, double>>>();
            dataSourceList = new List<Dictionary<string, IEnumerable<KeyValuePair<double, double>>>>();

            
        }

        public void Add(string name, IEnumerable<KeyValuePair<double, double>> chart)
        {
            dataSourceList.Add(lineSeries);
            Dictionary<string, IEnumerable<KeyValuePair<double, double>>> lastSeries = (from p in dataSourceList
                        select p).Last();
            lastSeries.Add(name, chart);
            Validate();

            lineSeries = new Dictionary<string, IEnumerable<KeyValuePair<double, double>>>();
        }

        public void Remove(string name)
        {
            lineSeries.Remove(name);
            Validate();
        }

        public void Clear()
        {
            foreach (var item in dataSourceList)
            {
                 item.Clear();
            }

            Validate();
        }

        private void Validate()
        {
            chartBase.Series.Clear();

            foreach (var item in dataSourceList)
            {
                foreach (var lineSerie in item)
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
}
