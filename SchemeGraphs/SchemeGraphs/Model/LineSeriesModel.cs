using System;
using System.Collections.Generic;

namespace SchemeGraphs.Model
{
    public class LineSeriesModel
    {
        public List<KeyValuePair<double,double>> FunctionPlots { get; set; }
        public List<KeyValuePair<double, double>> DerivativePlots { get; set; }
    }
}
