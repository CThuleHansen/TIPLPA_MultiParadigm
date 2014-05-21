using System;
using System.Collections.Generic;
using IronScheme.Runtime;
using SchemeLibrary.Loaders;

namespace SchemeLibrary.Math.Implementation
{
    public class FunctionPlotter : IFunctionPlotter
    {
        private readonly ISchemeEvaluator evaluator;

        public FunctionPlotter(ISchemeEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CreateFunctionSamplePairs {0} {1} {2} {3})", evaluator.Evaluate<Callable>(function), xBegin, xEnd, noOfSamples);
            var result = ConvertToPair(plots);
            return result;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, double dx, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CreateDerivativeFunctionSamplePairs {0} {1} {2} {3} {4})", evaluator.Evaluate<Callable>(function), dx, xBegin, xEnd, noOfSamples);
            var result = ConvertToPair(plots);
            return result;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, double xBegin, double xEnd, int noOfSamples)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<KeyValuePair<double, double>> ConvertToPair(Cons cons)
        {
            var result = new List<KeyValuePair<double, double>>();
            //TODO: Problem... cannot cast it to double always
            foreach (Cons pair in cons)
            {
                double x;
                double y;
                if (Double.TryParse(pair.car.ToString(), out x) && Double.TryParse(pair.cdr.ToString(), out y))
                {
                    result.Add(new KeyValuePair<double, double>(x, y));
                }
                else
                {
                    throw new InvalidCastException("Cannot parse cordinate to double");
                }

            }
            return result;
        }
    }
}
