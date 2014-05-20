using System;
using IronScheme.Runtime;

namespace SchemeLibrary.Math
{
    public interface IFunctionPlotter
    {
        Cons PlotFunction(string function, Int32 xBegin, Int32 xEnd ,Int32 noOfSamples);
        Cons PlotDerivative(string function, Int32 xBegin, Int32 xEnd, Int32 noOfSamples);
        Cons PlotIntegral(string function, Int32 xBegin, Int32 xEnd, Int32 noOfSamples);
    }
}
