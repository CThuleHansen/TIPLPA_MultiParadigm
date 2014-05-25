using System.Collections.Generic;
using System.Linq;
using IronScheme.Runtime;
using SchemeLibrary.Loaders;

namespace SchemeLibrary.Math.Implementation
{
    public class SchemeCalculator : IFunctionPlotter, ICalculate
    {
        private readonly ISchemeEvaluator evaluator;

        public SchemeCalculator(ISchemeEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CalcFuncPairs {0} {1} {2} {3})", evaluator.Evaluate<Callable>(function), xBegin, xEnd, noOfSamples);
            if(plots != null)
                return ConvertToPair(plots);
            return null;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, double dx, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CalcDeriFuncPairs {0} {1} {2} {3} {4})", evaluator.Evaluate<Callable>(function), dx, xBegin, xEnd, noOfSamples);
            if (plots != null)
                return ConvertToPair(plots);
            return null;
        }
        
        public IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CalcFuncPairs {0} {1} {2} {3})",
                evaluator.Evaluate<Callable>(function), xBegin, xEnd, noOfSamples);
            if (plots != null)
                return ConvertToPair(plots);
            return null;
        }

        private IEnumerable<KeyValuePair<double, double>> ConvertToPair(Cons cons)
        {
            var pairs = (from Cons pair in cons 
                         select new KeyValuePair<double, double>((double) pair.car, (double) pair.cdr));
            
            return pairs.ToList();
        }

        public double Integrate(string function, double xBegin, double xEnd, int rectangles)
        {
            var result = evaluator.Evaluate<double>("(CalcInt {0} {1} {2} {3})", evaluator.Evaluate<Callable>(function),
                xBegin, xEnd, rectangles);
            return result;
        }
    }
}
