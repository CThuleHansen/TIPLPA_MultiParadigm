using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SchemeLibrary.Loaders;
using SchemeLibrary.Loaders.Implementation;

namespace SchemeLibrary.Unit.Test
{
    public class LineSeriesTransformerTests
    {
        private ISchemeEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            var loader = new SchemeLoader();
            loader.Import("Scheme.rkt");
            evaluator = new ProxySchemeEvaluator();
        }
    }
}
