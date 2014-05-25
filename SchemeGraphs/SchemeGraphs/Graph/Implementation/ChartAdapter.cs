using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SchemeGraphs.Graph.Implementation
{
    public class ChartAdapter : IChart
    {
        private readonly PlotModel model;
        private readonly Dictionary<string, LineSeries> lineSeries;

        public ChartAdapter(PlotModel model)
        {
            this.model = model;
            lineSeries = new Dictionary<string, LineSeries>();
        }

        public void AddLineSeries(string name, IEnumerable<KeyValuePair<double, double>> points)
        {
            var currentLineSeries = ToLineSeries(points);
            lineSeries.Add(name, currentLineSeries);
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
            if (axis == AxisProperty.Y || axis == AxisProperty.Both)
            {
                model.Axes.Add(new LogarithmicAxis { Position = AxisPosition.Left, IsZoomEnabled = false});
            }
            if (axis == AxisProperty.X || axis == AxisProperty.Both)
            {
                model.Axes.Add(new LogarithmicAxis { Position = AxisPosition.Bottom, IsZoomEnabled = false });
            }
            Validate();
        }

        public void SetLinearScale(AxisProperty axis)
        {
            model.Axes.Clear();
            if (axis == AxisProperty.Y || axis == AxisProperty.Both)
            {
                model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, IsZoomEnabled = false});
            }
            if (axis == AxisProperty.X || axis == AxisProperty.Both)
            {
                model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, IsZoomEnabled = false });
            }
            Validate();
        }

        public void Clear()
        {
            lineSeries.Clear();
            Validate();
        }

        private void Validate()
        {
            model.Series.Clear();

            foreach (var item in lineSeries)
            {
                model.Series.Add(item.Value);
            }
            this.model.InvalidatePlot(true);
        }

        private LineSeries ToLineSeries(IEnumerable<KeyValuePair<double, double>> points)
        {
            var result = new LineSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
            };
            foreach (var pair in points)
            {
                result.Points.Add(new DataPoint(pair.Key, pair.Value));
            }
            return result;
        }
    }
}
