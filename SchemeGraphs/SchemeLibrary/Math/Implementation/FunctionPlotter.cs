using IronScheme.Runtime;
using IronScheme.Runtime.Typed;
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

        public Cons PlotFunction(string function, int xBegin, int xEnd, int noOfSamples)
        {
            var samples = evaluator.Evaluate<Cons>("(GenerateSamplePositions {0} {1} {2})", xBegin, xEnd, noOfSamples);
            var plotFunction = evaluator.Evaluate<Cons>("(CreateSamplePairs (lambda (x) (* x x)) '(1 2 3))");
            return plotFunction;
        }

        public Cons PlotDerivative(string function, int xBegin, int xEnd, int noOfSamples)
        {
            throw new System.NotImplementedException();
        }

        public Cons PlotIntegral(string function, int xBegin, int xEnd, int noOfSamples)
        {
            throw new System.NotImplementedException();
        }
    }
}
