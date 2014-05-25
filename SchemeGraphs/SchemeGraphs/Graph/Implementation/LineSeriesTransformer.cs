﻿using System;
using System.Linq;
using OxyPlot.Wpf;
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
                                      HasIntegral = viewModel.HasIntegral,
                                  };
            
            if (viewModel.HasDerivative)
            {
                result.DerivativePlots =
                    plotter.PlotDerivative(viewModel.Function, delta_x, x_min, x_max, samples).ToList();
            }

            if (viewModel.HasIntegral)
            {
                result.IntegralPlots = plotter.PlotIntegral(viewModel.Function, x_min, x_max, samples).ToList();
                result.IntegralValue = calculator.Integrate(viewModel.Function, x_min, x_max, samples);
            }

            return result;
        }
    }
}
