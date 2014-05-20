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

        public IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, int xBegin, int xEnd, int noOfSamples)
        {
            var proc = string.Format("(CreateSamplePairs {0} (GenerateSamplePositions {1} {2} {3}))", function, xBegin, xEnd, noOfSamples);
            //TODO: Somehow evaluate doesn't work with multi-args
            var plots = evaluator.Evaluate<Cons>(proc);

            return new List<KeyValuePair<double, double>>();
        }

        public IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, int xBegin, int xEnd, int noOfSamples)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, int xBegin, int xEnd, int noOfSamples)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<KeyValuePair<double, double>> ConvertToPair(Cons cons)
        {
            var result = new List<KeyValuePair<double, double>>();


            return result;
        }
    }
}
