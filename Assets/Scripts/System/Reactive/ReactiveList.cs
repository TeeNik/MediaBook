using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ReactiveList<T> : IList<T>, IDisposable
{
    private readonly List<Action<ReactiveList<T>, T[], T[]>> _handlers = new List<Action<ReactiveList<T>, T[], T[]>>();

    private readonly List<T> _innerList;

    private class TrampolineData
    {
        public readonly T[] Removed;
        public readonly T[] Added;

        public TrampolineData(T[] removed, T[] added)
        {
            Removed = removed;
            Added = added;
        }
    }

    private readonly Queue<TrampolineData> _trampoline = new Queue<TrampolineData>(32);

    private bool _inTrampoline;


    public ReactiveList()
    {
        _innerList = new List<T>();
    }

    public ReactiveList(List<T> innerList)
    {
        _innerList = innerList;
    }

    public void AddRange(IEnumerable<T> range)
    {
        var array = range.ToArray();
        _innerList.AddRange(array);
        OnNext(null, array);
    }

    public List<T> ToList()
    {
        return _innerList.ToList();
    }

    public T[] ToArray()
    {
        return _innerList.ToArray();
    }

    public T Find(Predicate<T> predicate)
    {
        return _innerList.Find(predicate);
    }


    public void RemoveAndAdd(IEnumerable<T> remove, IEnumerable<T> add)
    {
        var removed = new List<T>();
        foreach (var c in remove)
        {
            if (_innerList.Remove(c))
            {
                removed.Add(c);
            }
        }

        var added = new List<T>(add);
        _innerList.AddRange(added);
        OnNext(removed.Count > 0 ? removed.ToArray() : null, added.Count > 0 ? added.ToArray() : null);
    }

    public void RemoveAndAdd(Predicate<T> remove, IEnumerable<T> add)
    {
        var removed = new List<T>();
        for (var i = 0; i < _innerList.Count; ++i)
        {
            var t = _innerList[i];
            if (remove(t))
            {
                _innerList.RemoveAt(i);
                --i;
                removed.Add(t);
            }
        }

        var added = new List<T>(add);
        _innerList.AddRange(added);
        OnNext(removed.Count > 0 ? removed.ToArray() : null, added.Count > 0 ? added.ToArray() : null);
    }

    private void OnNext(T[] removed, T[] added)
    {
        var handlers = _handlers.ToArray();
        if (!_inTrampoline)
        {
            _inTrampoline = true;
            foreach (var handler in handlers)
            {
                handler(this, removed, added);
            }
            RunTrampoline();
        }
        else
        {
            _trampoline.Enqueue(new TrampolineData(removed, added));
        }
    }

    private void CheckAddedTwice(Action<ReactiveList<T>, T[], T[]> action)
    {
        if (_handlers.Contains(action))
        {
            throw new ArgumentException("action added twice: " + action);
        }
    }

    private void RunTrampoline()
    {
        var handlers = _handlers.ToArray();
        while (_trampoline.Count > 0)
        {
            var next = _trampoline.Dequeue();
            foreach (var handler in handlers)
            {
                handler(this, next.Removed, next.Added);
            }
        }
        _inTrampoline = false;
    }

    #region Implementation of IEnumerable

    public IEnumerator<T> GetEnumerator()
    {
        return _innerList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _innerList.GetEnumerator();
    }

    #endregion

    #region Implementation of ICollection<T>

    public void Add(T item)
    {
        _innerList.Add(item);
        OnNext(null, new[] { item });
    }

    public void Clear()
    {
        if (_innerList.Count > 0)
        {
            var removed = _innerList.ToArray();
            _innerList.Clear();
            OnNext(removed, null);
        }
    }

    public bool Contains(T item)
    {
        return _innerList.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _innerList.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        var result = _innerList.Remove(item);
        if (result)
        {
            OnNext(new[] { item }, null);
        }
        return result;
    }

    public bool RemoveAll(Predicate<T> predicate)
    {
        List<T> toRemove = null;

        foreach (var c in _innerList)
        {
            if (predicate(c))
            {
                if (toRemove == null)
                {
                    toRemove = new List<T>();
                }
                toRemove.Add(c);
            }
        }

        if (toRemove == null)
        {
            return false;
        }

        foreach (var c in toRemove)
        {
            _innerList.Remove(c);
        }

        OnNext(toRemove.ToArray(), null);
        return true;
    }

    public void Sort()
    {
        _innerList.Sort();
    }

    public void Sort(Comparison<T> comparison)
    {
        _innerList.Sort(comparison);
    }

    public int Count
    {
        get { return _innerList.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    #endregion

    #region Implementation of IList<T>

    public int IndexOf(T item)
    {
        return _innerList.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        _innerList.Insert(index, item);
        OnNext(null, new[] { item });
    }

    public void RemoveAt(int index)
    {
        var item = _innerList[index];
        _innerList.RemoveAt(index);
        OnNext(new[] { item }, null);
    }

    public T this[int index]
    {
        get { return _innerList[index]; }
        set
        {
            var old = _innerList[index];
            _innerList[index] = value;
            OnNext(new[] { old }, new[] { value });
        }
    }

    #endregion

    #region Implementation of IUnityObservable<ReactiveListCommand,T[]>

    public IDisposable Subscribe(Action<ReactiveList<T>, T[], T[]> observer, bool callObserver = true)
    {
        if (observer == null)
        {
            throw new ArgumentNullException("observer");
        }
        CheckAddedTwice(observer);
        if (callObserver)
        {
            observer(this, null, _innerList.ToArray());
        }

        _handlers.Add(observer);
        return new AnonymousDisposable(() => _handlers.Remove(observer));
    }

    #endregion

    #region Implementation of IDisposable

    public void Dispose()
    {
        _handlers.Clear();
    }

    #endregion

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var item in _innerList)
        {
            builder.Append(item).Append(" ");
        }

        if (_innerList.Count == 0)
        {
            builder.Append("Reactive list is empty!\n");
        }

        builder.Append("\n");
        return builder.ToString();
    }
}
