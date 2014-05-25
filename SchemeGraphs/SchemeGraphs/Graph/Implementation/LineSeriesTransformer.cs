using System;
using System.Linq;
using SchemeGraphs.Model;
using SchemeGraphs.ViewModels;
using SchemeLibrary.Math;

namespace SchemeGraphs.Graph.Implementation
{
    public class LineSeriesTransformer : ILineSeriesTranformer
    {
        private readonly IFunctionPlotter plotter;
        private readonly ICalculate calculator;
        public LineSeriesTransformer(IFunctionPlotter plotter, ICalculate calculator)
        {
            this.plotter = plotter;
            this.calculator = calculator;
        }

        /// <summary>
        /// Will throw an InvalidCastException if the viewmodel is not valid.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public LineSeriesModel Transform(LineSeriesViewModel viewModel)
        {
            LineSeriesModel result;
            string errorMessage = "";
            double x_min, x_max, delta_x;
            Int32 samples;
            bool errorOccured = false;
            if (double.TryParse(viewModel.XFrom, out x_min) == false)
            {
                errorMessage += "It was not possible to convert \"X min\" to a double.\n";
                errorOccured = true;
            }
            if (double.TryParse(viewModel.XTo, out x_max) == false)
            {
                errorMessage += "It was not possible to convert \"X max\" to a double.\n";
                errorOccured = true;
            }
            if (Int32.TryParse(viewModel.Samples, out samples) == false)
            {
                errorMessage += "It was not possible to convert \"Samples\" to an integer.\n";
                errorOccured = true;
            }
            if (errorOccured == false)
            {
                try
                {
                    result = new LineSeriesModel
                    {
                        FunctionPlots = plotter.PlotFunction(viewModel.Function, x_min, x_max, samples).ToList(),
                    };
                }
                catch (ArgumentNullException exception)
                { throw new ArgumentNullException("No result could be obtained.", exception); }



                if (viewModel.HasDerivative)
                {
                    if (double.TryParse(viewModel.Dx, out delta_x) == false)
                    {
                        errorMessage += "It was not possible to convert \"Dx\" to a double.\n";
                        errorOccured = true;
                    }
                    if (errorOccured == false)
                    {
                        try
                        {
                        result.DerivativePlots =
                            plotter.PlotDerivative(viewModel.Function, delta_x, x_min, x_max, samples).ToList();
                        }
                        catch (ArgumentNullException exception)
                        { throw new ArgumentNullException("No result could be obtained.", exception); }

                    }
                }
                if (errorOccured == false)
                    return result;
            }
            throw new ArgumentException(errorMessage);
        }

        public double CalculateIntegral(LineSeriesViewModel viewModel)
        {
            string errorMessage = "";
            bool errorOccured = false;
            double xfrom, xto;
            Int32 rectangles;
            if (double.TryParse(viewModel.XFrom, out xfrom) == false)
            {
                errorMessage += "It was not possible to convert \"X min\" to a double.\n";
                errorOccured = true;
            }
            if (double.TryParse(viewModel.XTo, out xto) == false)
            {
                errorMessage += "It was not possible to convert \"X max\" to a double.\n";
                errorOccured = true;
            }
            if (Int32.TryParse(viewModel.Rectangles, out rectangles) == false)
            {
                errorMessage += "It was not possible to convert \"Rectangles\" to an integers.\n";
                errorOccured = true;
            }
            if (errorOccured == false)
            {
                try
                {
                    return this.calculator.Integrate(viewModel.Function,
                        xfrom, xto, rectangles);
                }
                catch(NullReferenceException exception)
                { throw new NullReferenceException("No result could be obtained.", exception);}
            }
            else
            {
                throw new ArgumentException(errorMessage);
            }
        }

    }
}
