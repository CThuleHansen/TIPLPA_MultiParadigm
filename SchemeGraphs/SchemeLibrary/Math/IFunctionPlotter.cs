using System;
using System.Collections.Generic;

namespace SchemeLibrary.Math
{
    public interface IFunctionPlotter
    {
        /// <summary>
        /// Creates the plots for a function based on the scheme engine.
        /// </summary>
        /// <param name="function">Scheme procedure. e.g. (lambda (x) (* x x))</param>
        /// <param name="xBegin">Minimum value of x</param>
        /// <param name="xEnd">Maximum value of x</param>
        /// <param name="noOfSamples">Number of plots.</param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, double xBegin, double xEnd ,Int32 noOfSamples);
        
        /// <summary>
        /// Creates the plots of the derivative to the supplied function using the scheme engine.
        /// </summary>
        /// <param name="function">Scheme procedure. e.g. (lambda (x) (* x x))</param>
        /// <param name="dx">Delta x, which functions as epsilon (a small value)</param>
        /// <param name="xBegin">Minimum value of x</param>
        /// <param name="xEnd">Maximum value of x</param>
        /// <param name="noOfSamples">Number of plots.</param>        /// <returns></returns>
        IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, double dx, double xBegin, double xEnd, Int32 noOfSamples);
        
        /// <summary>
        /// Creates the plots for the boxes representing a naïve technique to calculate the integral of the function supplied. 
        /// </summary>
        /// <param name="function">Scheme procedure. e.g. (lambda (x) (* x x))</param>
        /// <param name="xBegin">Minimum value of x</param>
        /// <param name="xEnd">Maximum value of x</param>
        /// <param name="noOfSamples">Number of plots.</param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, double xBegin, double xEnd, Int32 noOfSamples);
    }
}
