using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronScheme;
using IronScheme.Hosting;
using IronScheme.Runtime;
using IronScheme.Scripting.Generation;

namespace CthConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"..\..\..\..\Scheme\Scheme.rkt";
            var schemeText = File.ReadAllText(path);
            schemeText.Eval();

            #region GenerateSamplePositions
            var GSP = "GenerateSamplePositions".Eval<Callable>();
            var GSPDelegate = GSP.ToDelegate<System.Func<object, object, object, IronScheme.Runtime.Cons>>();
            var GSPResult = GSPDelegate(1, 5, 5);
            Console.Write("GenerateSamplePositions test: (");
            foreach (var con in GSPResult)
            {
                Console.Write(con + " ");
            }
            Console.Write(")\r\n");
            #endregion

            #region CreateFunctionSamplePairs
            var CFSP = "CreateFunctionSamplePairs".Eval<Callable>();
            var CFSPDelegate = CFSP.ToDelegate<System.Func<object, object, object, object, IronScheme.Runtime.Cons>>();
            var CFSPResult = CFSPDelegate("(lambda (x) (* x x))".Eval(), 1, 3, 3);
            Console.Write("CreateFunctionSamplePairs test: (");
            foreach (var con in CFSPResult)
            {
                var conConverted = con as IronScheme.Runtime.Cons;
                Console.Write("(" + conConverted.car + "." + conConverted.cdr + ")");
            }
            Console.Write(")\r\n");
            #endregion

            #region CreateDerivativeFunctionSamplePairs

            var CDFSP = "CreateDerivativeFunctionSamplePairs".Eval<Callable>();
            var CDFSPDelegate = CDFSP.ToDelegate<System.Func<object, object, object, object, object, IronScheme.Runtime.Cons>>();
            var CDFSPResult = CDFSPDelegate("(lambda (x) (* x x))".Eval(),0.0001, 1, 3, 3);
            Console.Write("CreateDerivativeFunctionSamplePairs test: (");
            foreach (var con in CDFSPResult)
            {
                var conConverted = con as IronScheme.Runtime.Cons;
                Console.Write("(" + conConverted.car + "." + conConverted.cdr + ")");
            }
            Console.Write(")\r\n");
            #endregion

            #region CalculateIntegrationValue
            var CIV = "CalculateIntegrationValue".Eval<Callable>();
            var CIVDelegate = CIV.ToDelegate<System.Func<object, object, object, object, IronScheme.Runtime.Fraction>>();
            var CIVResult = CIVDelegate("(lambda (x) (* x x))".Eval(), 1, 3, 5000);
            Console.Write("CalculateIntegrationValue test: " + CIVResult.Numerator.ToFloat64()/CIVResult.Denominator.ToFloat64());
            Console.Write("\r\n");
            #endregion

            Console.ReadLine();
        }
    }
}
