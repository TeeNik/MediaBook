using System;
using System.Collections.Generic;
using System.Linq;

public sealed class CompositeDisposable : IDisposable
{
    private readonly object _gate = new object();

    class Entry
    {
        public IDisposable Disposable;
        public string Tag;

        public Entry(IDisposable disposable, string tag = null)
        {
            Disposable = disposable;
            Tag = tag;
        }
    }

    private bool _disposed;
    private List<Entry> _disposables;
    private int _count;
    private const int SHRINK_THRESHOLD = 64;

    public CompositeDisposable()
    {
        _disposables = new List<Entry>();
    }

    public CompositeDisposable(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException("capacity");

        _disposables = new List<Entry>(capacity);
    }

    public int Count => _count;

    public void Add(IDisposable item, string tag = null)
    {
        if (item == null)
            throw new ArgumentNullException("item");

        var shouldDispose = false;
        lock (_gate)
        {
            shouldDispose = _disposed;
            if (!_disposed)
            {
                _disposables.Add(new Entry(item, tag));
                _count++;
            }
        }
        if (shouldDispose)
            item.Dispose();
    }

    public void RemoveAll(string tag)
    {
        var toRemove = _disposables.Where(x => x.Tag == tag).ToArray();
        foreach (var c in toRemove)
        {
            Remove(c.Disposable);
        }
    }

    public bool Remove(IDisposable item)
    {
        if (item == null)
            throw new ArgumentNullException("item");

        var shouldDispose = false;

        lock (_gate)
        {
            if (!_disposed)
            {
                var i = 0;
                for (; i < _disposables.Count; ++i)
                {
                    if (_disposables[i].Disposable == item)
                    {
                        break;
                    }
                }
                if (i < _disposables.Count)
                {
                    shouldDispose = true;
                    _disposables[i] = null;
                    _count--;

                    if (_disposables.Capacity > SHRINK_THRESHOLD && _count < _disposables.Capacity / 2)
                    {
                        var old = _disposables;
                        _disposables = new List<Entry>(_disposables.Capacity / 2);

                        foreach (var d in old)
                            if (d != null)
                                _disposables.Add(d);
                    }
                }
            }
        }

        if (shouldDispose)
            item.Dispose();

        return shouldDispose;
    }

    public void Dispose()
    {
        var currentDisposables = default(Entry[]);
        lock (_gate)
        {
            if (!_disposed)
            {
                _disposed = true;
                currentDisposables = _disposables.ToArray();
                _disposables.Clear();
                _count = 0;
            }
        }

        if (currentDisposables != null)
        {
            foreach (var d in currentDisposables)
            {
                if (d.Disposable != null)
                {
                    d.Disposable.Dispose();
                }
            }
        }
    }

    public void Clear()
    {
        var currentDisposables = default(Entry[]);
        lock (_gate)
        {
            currentDisposables = _disposables.ToArray();
            _disposables.Clear();
            _count = 0;
        }

        foreach (var d in currentDisposables)
        {
            if (d.Disposable != null)
            {
                d.Disposable.Dispose();
            }
        }
    }

    public void CopyTo(IDisposable[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException("array");
        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException("arrayIndex");

        lock (_gate)
        {
            Array.Copy(_disposables.Where(d => d != null).ToArray(), 0, array, arrayIndex, array.Length - arrayIndex);
        }
    }
}
