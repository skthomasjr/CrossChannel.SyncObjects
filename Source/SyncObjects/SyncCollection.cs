using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SyncObjects
{
    public class SyncCollection<T> : ObservableCollection<T>
    {
        private int activePeerCount;
        private IList<SyncItem<T>> synchronized = new List<SyncItem<T>>();
        private IList<SyncItem<T>> proposed = new List<SyncItem<T>>();
        private IList<SyncItem<T>> local = new List<SyncItem<T>>();

        //public SyncItem<T> LastSyncItem => synchronizedItems.Any() ? synchronizedItems[synchronizedItems.Count - 1] : null;

        //public SyncCollection()
        //{
        //    CollectionChanged += OnCollectionChanged;
        //}

        //private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        //{
        //    if (args.NewItems != null)
        //    {
        //        foreach (var item in args.NewItems)
        //        {
        //        }
        //    }

        //    if (args.OldItems != null)
        //    {
        //        foreach (var item in args.OldItems)
        //        {
        //        }
        //    }
        //}
    }
}