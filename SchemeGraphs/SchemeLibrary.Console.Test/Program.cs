using System;

namespace SchemeLibrary.Console.Test
{
    public class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Hello Rasmus from Git");

            System.Console.WriteLine("Loading scheme file");
            var loader = new SchemeLoader();
            loader.Import("simple.ss");
            System.Console.WriteLine("Finished loading scheme file");


            System.Console.WriteLine("Evaluating scheme statement");
            var evaluator = new SchemeEvalProxy();
            var result = evaluator.Evaluate<Int32>("(increment 5)");

            System.Console.WriteLine("The result is: {0}", result);

            System.Console.ReadLine();
        }
    }
}
