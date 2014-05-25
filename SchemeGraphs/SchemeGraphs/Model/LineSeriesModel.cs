using System;
using System.Collections.Generic;

namespace SchemeGraphs.Model
{
    public class LineSeriesModel
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string Function { get; set; }
        public bool HasDerivative { get; set; }
        public bool HasIntegral { get; set; }

        public List<KeyValuePair<double,double>> FunctionPlots { get; set; }
        public List<KeyValuePair<double, double>> DerivativePlots { get; set; }
        public List<KeyValuePair<double, double>> IntegralPlots { get; set; }
        public double IntegralValue { get; set; }
    }
}
