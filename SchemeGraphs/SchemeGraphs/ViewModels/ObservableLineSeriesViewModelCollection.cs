using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SchemeGraphs.ViewModels
{
    public class ObservableLineSeriesViewModelCollection : ObservableCollection<LineSeriesViewModel>
    {
        public void AddModel(LineSeriesViewModel model)
        {
            model.PropertyChanged += (sender, args) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Add(model);
        }

        public void RemoveModel(LineSeriesViewModel model)
        {
            model.PropertyChanged -= (sender, args) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Remove(model);
        }

        public void ClearModel()
        {
            foreach (var model in this)
            {
                model.PropertyChanged -= (sender, args) => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            Clear();
        }
    }
}
