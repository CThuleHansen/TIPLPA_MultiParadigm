using System.Collections.ObjectModel;

namespace SchemeGraphs.Model
{
    /// <summary>
    /// This observable collection only raises the collection changed event when the collection changes.
    /// </summary>
    public class ObservableLineSeriesModelCollection : ObservableCollection<LineSeriesModel>
    {
    }
}