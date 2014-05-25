using System;

namespace SchemeLibrary.Math
{
    public interface ICalculate
    {
        double Integrate(string function, double xBegin, double xEnd, Int32 samples);
    }
}
