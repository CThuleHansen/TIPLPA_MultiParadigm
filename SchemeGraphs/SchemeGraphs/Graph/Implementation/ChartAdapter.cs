using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SchemeGraphs.Graph.Implementation
{
    public class ChartAdapter : IChart
    {
        private readonly PlotModel model;
        private Dictionary<string, LineSeries> lineSeries;
        private List<Dictionary<string, LineSeries>> dataSourceList;

        public ChartAdapter(PlotModel model)
        {
            this.model = model;
            lineSeries = new Dictionary<string, LineSeries>();
            dataSourceList = new List<Dictionary<string, LineSeries>>();
        }

        public void AddLineSeries(string name, IEnumerable<KeyValuePair<double, double>> points, bool log)
        {
            
            
            dataSourceList.Add(lineSeries);
            var lastSeries = (from p in dataSourceList
                              select p).Last();

            lastSeries.Add(name, ToLineSeries(points));
            Validate();

            lineSeries = new Dictionary<string, LineSeries>();
        }

        public void AddIntegralBoxes(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string name)
        {
            lineSeries.Remove(name);
            Validate();
        }

        public void SetLogrithmicScale(AxisProperty axis)
        {
            model.Axes.Clear();
            model.Axes.Add(new LogarithmicAxis());
        }

        public void SetLinearScale(AxisProperty axis)
        {
            //TODO: set focus of the axis so you can see the plots
            model.Axes.Clear();
            model.Axes.Add(new LinearAxis());

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
            model.Series.Clear();

            foreach (var item in dataSourceList)
            {
                foreach (var series in item)
                {
                    model.Series.Add(series.Value);
                }
            }
            this.model.InvalidatePlot(true);
        }

        private LineSeries ToLineSeries(IEnumerable<KeyValuePair<double, double>> points)
        {
            var result = new LineSeries();
            foreach (var pair in points)
            {
                result.Points.Add(new DataPoint(pair.Key, pair.Value));
            }
            return result;
        }
    }
}
