using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SchemeGraphs.Graph.Implementation
{
    public class ChartAdapter : IChart
    {
        private readonly PlotModel model;
        private Dictionary<string,LineSeries> lineSeries; 

        public ChartAdapter(PlotModel model)
        {
            this.model = model;
            lineSeries = new Dictionary<string, LineSeries>();
        }

        public void AddLineSeries(string name, IEnumerable<KeyValuePair<double, double>> points)
        {
            lineSeries.Add(name,ToLineSeries(points));
            Validate();
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
            lineSeries.Clear();
            Validate();
        }

        private void Validate()
        {
            model.Series.Clear();


            foreach (var series in lineSeries)
            {
                model.Series.Add(series.Value);
            }
        }

        private LineSeries ToLineSeries(IEnumerable<KeyValuePair<double, double>> points)
        {
            var result = new LineSeries();
            foreach (var pair in points)
            {
                result.Points.Add(new DataPoint(pair.Key,pair.Value));
            }
            return result;
        }
    }
}
