using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SchemeGraphs.Graph.Implementation
{
    public class ChartAdapter : IChart
    {
        private readonly PlotModel model;
        private readonly Dictionary<string, Series> series;

        public ChartAdapter(PlotModel model)
        {
            this.model = model;
            series = new Dictionary<string, Series>();
        }

        public void AddLineSeries(string name, IEnumerable<KeyValuePair<double, double>> points, OxyColor color)
        {
            var lineSeries = ToLineSeries(points, color);
            series.Add(name, lineSeries);
            Validate();
        }

        public void AddIntegralBoxes(string name, IEnumerable<KeyValuePair<double, double>> points, double integralValue)
        {
            series.Add(name, ToAreaSeries(points,integralValue));
            Validate();
        }


        public void Remove(string name)
        {
            series.Remove(name);
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
            series.Clear();
            Validate();
        }

        private void Validate()
        {
            model.Series.Clear();

            foreach (var item in series)
            {
                model.Series.Add(item.Value);
            }
            this.model.InvalidatePlot(true);
        }

        private LineSeries ToLineSeries(IEnumerable<KeyValuePair<double, double>> points, OxyColor color)
        {
            var result = new LineSeries()
            {
                Color = color,
                MarkerType = MarkerType.Circle,
                MarkerStrokeThickness = 10,
                MarkerSize = 6,
            };
            foreach (var pair in points)
            {
                result.Points.Add(new DataPoint(pair.Key, pair.Value));
            }
            return result;
        }

        private AreaSeries ToAreaSeries(IEnumerable<KeyValuePair<double, double>> points, double integralValue)
        {
            var result = new AreaSeries
            {
                Title = integralValue.ToString(),
            };
            var p = points.ToList();
            result.Points.Add(new DataPoint(p[0].Key,0));
            for (int i = 0; i < p.Count-1; i++)
            {
                result.Points.Add(new DataPoint(p[i].Key, p[i + 1].Value));
                result.Points.Add(new DataPoint(p[i + 1].Key, p[i + 1].Value));
            }
            result.Points.Add(new DataPoint(p[p.Count-1].Key,0));
            result.Points.Add(new DataPoint(p[0].Key, 0));

            return result;
        }
    }
}
