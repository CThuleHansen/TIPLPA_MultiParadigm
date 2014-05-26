using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SchemeLibrary.Loaders;
using SchemeLibrary.Loaders.Implementation;
using SchemeLibrary.Math;
using SchemeLibrary.Math.Implementation;

namespace SchemeLibrary.Unit.Test
{
    public class SchemeCalculatorTests
    {
        private ISchemeEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            var loader = new SchemeLoader();
            loader.Import("Scheme.rkt");
            evaluator = new ProxySchemeEvaluator();
        }

        [Test]
        public void IntegrateWith5RectanglesAtSpecificPosition()
        {
            // Arrange
            var schemeCalc = new SchemeCalculator(evaluator) as ICalculate;
            double expected = 0.0;

            // Act
            var actual = schemeCalc.Integrate("(lambda (x) (* x x))",1,1,5);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IntegrateWith2RectsFrom1To5()
        {
            // Arrange
            var schemeCalc = new SchemeCalculator(evaluator) as ICalculate;
            double expected = 20.0;

            // Act
            var actual = schemeCalc.Integrate("(lambda (x) (* x x))", 1, 5, 2);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void IntegrateWith0RectsFrom1To5()
        {
            // Arrange
            var schemeCalc = new SchemeCalculator(evaluator) as ICalculate;
            double expected = 20.0;

            // Act
            schemeCalc.Integrate("(lambda (x) (* x x))", 1, 5, 0);
        }

    }
}
