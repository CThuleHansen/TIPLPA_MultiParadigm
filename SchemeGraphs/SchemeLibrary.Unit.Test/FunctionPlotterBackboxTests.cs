using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SchemeLibrary.Loaders;
using SchemeLibrary.Loaders.Implementation;
using SchemeLibrary.Math.Implementation;

namespace SchemeLibrary.Unit.Test
{
    [TestFixture]
    public class FunctionPlotterBackboxTests
    {
        private ISchemeEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            var loader = new SchemeLoader();
            loader.Import("Scheme.rkt");
            evaluator = new ProxySchemeEvaluator();
        }

        #region plot normal function
        [Test]
        public void PlotSquaredFunctionFrom1To3()
        {
            // Arrange
            var plotter = new SchemeCalculator(evaluator);
            var expected = new List<KeyValuePair<double, double>>
            {
                new KeyValuePair<double, double>(1, 1),
                new KeyValuePair<double, double>(2, 4),
                new KeyValuePair<double, double>(3, 9),
            };

            // Act
            var actual = plotter.PlotFunction("(lambda (x) (* x x))", 1, 3, 3);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PlotLinearFunctionFrom1To3()
        {
            // Arrange
            var plotter = new SchemeCalculator(evaluator);
            var expected = new List<KeyValuePair<double, double>>
            {
                new KeyValuePair<double, double>(1, 1),
                new KeyValuePair<double, double>(2, 2),
                new KeyValuePair<double, double>(3, 3),
            };

            // Act
            var actual = plotter.PlotFunction("(lambda (x) x)", 1, 3, 3);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PlotLinearFunctionFrom1To3In5Points()
        {
            // Arrange
            var plotter = new SchemeCalculator(evaluator);
            var expected = new List<KeyValuePair<double, double>>
            {
                new KeyValuePair<double, double>(1, 1),
                new KeyValuePair<double, double>(1.5, 1.5),
                new KeyValuePair<double, double>(2, 2),
                new KeyValuePair<double, double>(2.5, 2.5),
                new KeyValuePair<double, double>(3, 3),
            };

            // Act
            var actual = plotter.PlotFunction("(lambda (x) x)", 1, 3, 5);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PlotLinearFunctionsFrom1To3InNegativeSamples()
        {
            // Arrange
            var plotter = new SchemeCalculator(evaluator);


            // Act
            var actual = plotter.PlotFunction("(lambda (x) x)", 1, 3, -1);

            // Assert
            Assert.AreEqual(actual, null);
        }
        #endregion

        #region derivative
        [Test]
        public void PlotDerivativeWithDx0()
        {
            // Arrange
            var plotter = new SchemeCalculator(evaluator);


            // Act
            var actual = plotter.PlotDerivative("(lambda (x) (* x x))", 0, 1, 3, 3);

            // Assert
            Assert.AreEqual(actual, null);
        }

        [Test]
        public void PlotDerivative1To33Samples()
        {
            // Arrange
            double dx = 0.001;
            var plotter = new SchemeCalculator(evaluator);
            var expected = new List<KeyValuePair<double, double>>
            {
                new KeyValuePair<double, double>(1, 2),
                new KeyValuePair<double, double>(2, 4),
                new KeyValuePair<double, double>(3, 6),
            };

            // Act
            var actual = plotter.PlotDerivative("(lambda (x) (* x x))", dx, 1, 3, 3);

            // Assert
            for (int i = 0; i < 3; i++)
            {
                var expectedI = expected[i];
                var actualI = actual.ElementAt(i);
                Assert.That(actualI.Value, Is.EqualTo(expectedI.Value).Within(dx));
                Assert.AreEqual(expectedI.Key, actualI.Key);
            }
        }
        #endregion

    }
}
