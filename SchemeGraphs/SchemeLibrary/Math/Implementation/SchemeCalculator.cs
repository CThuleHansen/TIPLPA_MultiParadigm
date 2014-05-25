﻿using System.Collections.Generic;
using System.Linq;
using IronScheme.Runtime;
using SchemeLibrary.Loaders;

namespace SchemeLibrary.Math.Implementation
{
    public class SchemeCalculator : IFunctionPlotter, ISchemeCalculator
    {
        private readonly ISchemeEvaluator evaluator;

        public SchemeCalculator(ISchemeEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotFunction(string function, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CalcFuncPairs {0} {1} {2} {3})", evaluator.Evaluate<Callable>(function), xBegin, xEnd, noOfSamples);
            var result = ConvertToPair(plots);
            return result;
        }

        public IEnumerable<KeyValuePair<double, double>> PlotDerivative(string function, double dx, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CalcDeriFuncPairs {0} {1} {2} {3} {4})", evaluator.Evaluate<Callable>(function), dx, xBegin, xEnd, noOfSamples);
            var result = ConvertToPair(plots);
            return result;
        }
        
        public IEnumerable<KeyValuePair<double, double>> PlotIntegral(string function, double xBegin, double xEnd, int noOfSamples)
        {
            var plots = evaluator.Evaluate<Cons>("(CalcFuncPairs {0} {1} {2} {3})",
                evaluator.Evaluate<Callable>(function), xBegin, xEnd, noOfSamples);
            var result = ConvertToPair(plots);
            return result;
        }

        private IEnumerable<KeyValuePair<double, double>> ConvertToPair(Cons cons)
        {
            var pairs = (from Cons pair in cons 
                         select new KeyValuePair<double, double>((double) pair.car, (double) pair.cdr));
            
            return pairs.ToList();
        }

        public double CalculateIntegral(string function, double xBegin, double xEnd, int noOfSamples)
        {
            var result = evaluator.Evaluate<double>("(CalcInt {0} {1} {2} {3})", evaluator.Evaluate<Callable>(function),
                xBegin, xEnd, noOfSamples);
            return result;
        }

    }
}
