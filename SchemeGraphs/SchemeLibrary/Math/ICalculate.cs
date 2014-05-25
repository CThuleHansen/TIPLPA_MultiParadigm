using System;

namespace SchemeLibrary.Math
{
    public interface ICalculate
    {
        /// <summary>
        /// Calculates the integral of a function based on the scheme engine.
        /// </summary>
        /// <param name="function">Scheme procedure.</param>
        /// <param name="xBegin">Minimum value of x.</param>
        /// <param name="xEnd">Maximum value of x.</param>
        /// <param name="rectangles">Number of rectangles.</param>
        /// <returns></returns>
        double Integrate(string function, double xBegin, double xEnd, Int32 rectangles);
    }
}
