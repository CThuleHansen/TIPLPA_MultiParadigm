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
            var x_min = Double.Parse(viewModel.XFrom);
            var x_max = Double.Parse(viewModel.XTo);
            var samples = Int32.Parse(viewModel.Samples);
            var delta_x = Double.Parse(viewModel.Dx);

            var result = new LineSeriesModel
                                  {
                                      Name = viewModel.Name,
                                      Function = viewModel.Function,
                                      FunctionPlots = plotter.PlotFunction(viewModel.Function,x_min,x_max,samples).ToList(),
                                      HasDerivative = viewModel.HasDerivative,
                                  };
            
            if (viewModel.HasDerivative)
            {
                result.DerivativePlots =
                    plotter.PlotDerivative(viewModel.Function, delta_x, x_min, x_max, samples).ToList();
            }

            return result;
        }
    }
}
