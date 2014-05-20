using System;
using System.Collections.Generic;

namespace SchemeLibrary.Math
{
    public interface IFunctionPlotter
    {
        IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, Int32 xBegin, Int32 xEnd ,Int32 noOfSamples);
        IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, Int32 xBegin, Int32 xEnd, Int32 noOfSamples);
        IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, Int32 xBegin, Int32 xEnd, Int32 noOfSamples);
    }
}
