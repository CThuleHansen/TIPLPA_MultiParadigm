namespace SchemeLibrary.Math
{
    public interface ISchemeCalculator
    {
        double CalculateIntegral(string function, double xBegin, double xEnd, int noOfSamples);
    }
}