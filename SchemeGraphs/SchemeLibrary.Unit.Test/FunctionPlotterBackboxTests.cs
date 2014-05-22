using System.Collections.Generic;
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

        [Test]
        public void PlotSquaredFunctionFrom1To3()
        {
            // Arrange
            var plotter = new FunctionPlotter(evaluator);
            var expected = new List<KeyValuePair<double, double>>
            {
                new KeyValuePair<double, double>(1, 1),
                new KeyValuePair<double, double>(2, 4),
                new KeyValuePair<double, double>(3, 9),
            };

            // Act
            var actual = plotter.PlotFunction("(lambda (x) (* x x))", 1, 3, 3);

            // Assert
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void PlotLinearFunctionFrom1To3()
        {
            // Arrange
            var plotter = new FunctionPlotter(evaluator);
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
            var plotter = new FunctionPlotter(evaluator);
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
    }
}
