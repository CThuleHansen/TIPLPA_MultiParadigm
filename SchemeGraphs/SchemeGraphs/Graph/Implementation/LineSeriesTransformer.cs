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
            LineSeriesModel result = new LineSeriesModel
                                     {
                                         Uid = viewModel.Uid,
                                         Name = viewModel.Name,
                                         Function = viewModel.Function,
                                         HasDerivative = viewModel.HasDerivative,
                                         HasIntegral = viewModel.HasIntegral,
                                     };
            string errorMessage = string.Empty;
            double x_min, x_max, delta_x;
            Int32 samples, rectangles;
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
                    var plots = plotter.PlotFunction(viewModel.Function, x_min, x_max, samples);
                    if (plots != null)
                    {
                        result.FunctionPlots = plots.ToList();
                    }
                    else
                    {
                        throw new ArgumentException("No result could be obtained.");
                    }
                }

                if (viewModel.HasDerivative)
                {
                    if (double.TryParse(viewModel.Dx, out delta_x) == false)
                    {
                        errorMessage += "It was not possible to convert \"Dx\" to a double.\n";
                        errorOccured = true;
                    }
                    if (errorOccured == false)
                    {
                        var plots = plotter.PlotDerivative(viewModel.Function, delta_x, x_min, x_max, samples);
                        if(plots != null)
                            result.DerivativePlots = plots.ToList();
                        else
                        {
                            throw new ArgumentException("No result could be obtained.");
                        }


                    }
                }
                if (viewModel.HasIntegral)
                {
                    if (Int32.TryParse(viewModel.Rectangles, out rectangles) == false)
                    {
                        errorMessage += "It was not possible to convert \"Dx\" to a double.\n";
                        errorOccured = true;
                    }
                    if (errorOccured == false)
                    {
                        double value = 0.0;
                        var plots = plotter.PlotIntegral(viewModel.Function, x_min, x_max, rectangles);
                        try
                        {
                            value = calculator.Integrate(viewModel.Function, x_min, x_max, rectangles);
                        }
                        catch (NullReferenceException ex)
                        {
                            throw new ArgumentException("No result could be obtained.", ex);
                        }
                        if (plots != null)
                        {
                            result.IntegralPlots = plots.ToList();
                            result.IntegralValue = value;
                        }
                        else
                        {
                            throw new ArgumentException("No result could be obtained.");
                        }
                    }
                }

                if (errorOccured == false)
                    return result;
                else
                   throw new ArgumentException(errorMessage);
        }
    }
}
