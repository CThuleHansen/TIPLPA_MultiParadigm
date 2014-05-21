using System;
using SchemeLibrary.Loaders.Implementation;
using SchemeLibrary.Math.Implementation;

namespace SchemeLibrary.Console.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Loading scheme file");
            var loader = new SchemeLoader();
            loader.Import("simple.ss");
            loader.Import("Scheme.rkt");
            System.Console.WriteLine("Finished loading scheme file");


            System.Console.WriteLine("Evaluating scheme statement");
            var evaluator = new ProxySchemeEvaluator();
            var result = evaluator.Evaluate<Int32>("(increment {0})",5);
            System.Console.WriteLine("The result is: {0}", result);
            var plotter = new FunctionPlotter(evaluator);
            var result2 = plotter.PlotFunction("(lambda (x) (* x x))", 1.6, 11.2, 10);


            System.Console.ReadLine();
        }
    }
}
