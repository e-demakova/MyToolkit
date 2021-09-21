using System;
using System.Collections;
using System.Collections.Generic;

namespace Deblue.ObservingSystem
{
    public interface IReadonlyObservList<T>
    {
        int Count { get; }
        int Capacity { get; }
        public T this[int i] { get; }
        
        IObserver SubscribeOnAdding(Action<ValueAdded<int, T>> action, List<IObserver> observers = null);
        IObserver SubscribeOnRemoving(Action<ValueRemoved<int, T>> action, List<IObserver> observers = null);
        IObserver SubscribeOnChanging(Action<ValueChanged<int, T>> action, List<IObserver> observers = null);

        void UnsubscribeOnAdding(Action<ValueAdded<int, T>> action);
        void UnsubscribeOnRemoving(Action<ValueRemoved<int, T>> action);
        void UnsubscribeOnChanging(Action<ValueChanged<int, T>> action);
    }

    [Serializable]
    public class ObservList<T> : EventSender, IReadonlyObservList<T>, IList<T>
    {
        public int Count => _list.Count;
        public int Capacity => _list.Capacity;
        public bool IsReadOnly => ((ICollection<T>) _list).IsReadOnly;

        private List<T> _list;

        public ObservList() : this(0)
        {
        }

        public ObservList(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public T this[int i]
        {
            get { return _list[i]; }
            set
            {
                Raise(new ValueChanged<int, T>(i, _list[i], value));
                _list[i] = value;
            }
        }

        public void Add(T item)
        {
            _list.Add(item);
            Raise(new ValueAdded<int, T>(_list.Count, item));
        }

        public void Insert(int index, T item)
        {
            Insert(index, item);
            Raise(new ValueAdded<int, T>(index, item));
        }

        public void Remove(T item)
        {
            var index = _list.IndexOf(item);
            _list.Remove(item);
            Raise(new ValueRemoved<int, T>(index, item));
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            _list.RemoveAt(index);
            Raise(new ValueRemoved<int, T>(index, item));
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            return _list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void Clear()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                Remove(_list[i]);
            }

            ClearSubscribers();
        }

        #region Subscribing

        public IObserver SubscribeOnAdding(Action<ValueAdded<int, T>> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        public IObserver SubscribeOnRemoving(Action<ValueRemoved<int, T>> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        public IObserver SubscribeOnChanging(Action<ValueChanged<int, T>> action, List<IObserver> observers = null)
        {
            return Subscribe(action, observers);
        }

        #endregion

        #region Unsubscribing

        public void UnsubscribeOnAdding(Action<ValueAdded<int, T>> action)
        {
            Unsubscribe(action);
        }

        public void UnsubscribeOnRemoving(Action<ValueRemoved<int, T>> action)
        {
            Unsubscribe(action);
        }

        public void UnsubscribeOnChanging(Action<ValueChanged<int, T>> action)
        {
            Unsubscribe(action);
        }

        #endregion
    }
}