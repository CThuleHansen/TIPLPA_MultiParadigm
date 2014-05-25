using IronScheme.Runtime;
using SchemeLibrary.Loaders;

namespace SchemeLibrary.Math.Implementation
{
    public class Calculator : ICalculate
    {
        private readonly ISchemeEvaluator evaluator;

        public Calculator(ISchemeEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public double Integrate(string function, double xBegin, double xEnd, int samples)
        {
            var result = evaluator.Evaluate<double>("(CalcInt {0} {1} {2} {3})", evaluator.Evaluate<Callable>(function),
                xBegin, xEnd, samples);
            return result;
        }
    }
}
