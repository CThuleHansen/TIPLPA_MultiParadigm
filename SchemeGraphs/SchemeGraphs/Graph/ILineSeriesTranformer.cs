using SchemeGraphs.Model;
using SchemeGraphs.ViewModels;

namespace SchemeGraphs.Graph
{
    public interface ILineSeriesTranformer
    {
        LineSeriesModel Transform(LineSeriesViewModel viewModel);
    }
}
