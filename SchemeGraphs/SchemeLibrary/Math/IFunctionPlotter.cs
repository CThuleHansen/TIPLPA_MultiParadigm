using System;
using System.Collections.Generic;

namespace SchemeLibrary.Math
{
    public interface IFunctionPlotter
    {
        IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, double xBegin, double xEnd ,Int32 noOfSamples);
        IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, double dx, double xBegin, double xEnd, Int32 noOfSamples);
        IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, double xBegin, double xEnd, Int32 noOfSamples);
    }
}
