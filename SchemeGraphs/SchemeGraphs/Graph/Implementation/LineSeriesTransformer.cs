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

        public LineSeriesTransformer(IFunctionPlotter plotter)
        {
            this.plotter = plotter;
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
                result = new LineSeriesModel
                {
                    FunctionPlots = plotter.PlotFunction(viewModel.Function, x_min, x_max, samples).ToList(),
                };


                if (viewModel.HasDerivative)
                {
                    if (double.TryParse(viewModel.Dx, out delta_x) == false)
                    {
                        errorMessage += "It was not possible to convert \"Dx\" to a double.\n";
                        errorOccured = true;
                    }
                    if(errorOccured == false)
                    result.DerivativePlots =
                        plotter.PlotDerivative(viewModel.Function, delta_x, x_min, x_max, samples).ToList();
                }
                if (errorOccured == false)
                    return result;
            }
            throw new ArgumentException(errorMessage);
        }
        

    }
}
