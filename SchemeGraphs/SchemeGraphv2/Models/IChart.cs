using System.Collections;
using System.Collections.Generic;

namespace SchemeGraphv2.Models
{
    /// <summary>
    /// High level interface for handling graphs.
    /// </summary>
    public interface IChart
    {
        /// <summary>
        /// Adds a new line series to the existing graph. 
        /// </summary>
        /// <param name="name">Identifier</param>
        /// <param name="chart">Points in the line series</param>
        void Add(string name, IEnumerable<KeyValuePair<double, double>> chart);
        
        /// <summary>
        /// Removes a line serie from the graph.
        /// </summary>
        /// <param name="name">Identifier</param>
        void Remove(string name);

        /// <summary>
        /// Removes all existing line series
        /// </summary>
        void Clear();
    }
}