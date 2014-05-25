using System.Collections.Generic;

namespace SchemeGraphs.Graph
{
    public interface IChart
    {
        /// <summary>
        /// Adds a lineseries to the graph using a collection of keyvaluepairs of <x,y> values.
        /// </summary>
        /// <param name="name">Identifier for the series.</param>
        /// <param name="points">Points plotted.</param>
        void AddLineSeries(string name, IEnumerable<KeyValuePair<double,double>> points);

        /// <summary>
        /// Graphs boxes that span the area under a line series representing the integral.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="points"></param>
        /// <param name="integralValue"></param>
        void AddIntegralBoxes(string name, IEnumerable<KeyValuePair<double, double>> points, double integralValue);

        /// <summary>
        /// Removes a component from the graph by its identifier.
        /// </summary>
        /// <param name="name">Identifier.</param>
        void Remove(string name);

        void SetLogrithmicScale(AxisProperty axis);

        void SetLinearScale(AxisProperty axis);

        /// <summary>
        /// Removes all components of the graph.
        /// </summary>
        void Clear();
    }

    public enum AxisProperty
    {
        X,
        Y,
        Both
    }
}
