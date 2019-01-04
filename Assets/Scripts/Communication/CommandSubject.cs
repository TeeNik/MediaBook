using System;
using System.Collections.Generic;

public delegate void Callback<T>(T arg1);

public class CommandSubject 
{
    public Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Callback<T> handler)
    {
        Type eventType = typeof(T);
        if (!_eventTable.ContainsKey(eventType))
        {
            _eventTable.Add(eventType, null);
        }
        _eventTable[eventType] = (Callback<T>)_eventTable[eventType] + handler;
    }

    private void RemoveListener<T>(Delegate handler)
    {
        Type eventType = typeof(T);

        if (_eventTable.ContainsKey(eventType))
        {
            _eventTable[eventType] = Delegate.Remove(_eventTable[eventType], handler);
            if (_eventTable[eventType] == null)
            {
                _eventTable.Remove(eventType);
            }
        }
    }

    public void Broadcast<T>(T arg)
    {
        Type eventType = typeof(T);
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d))
        {
            Callback<T> callback = d as Callback<T>;
            callback?.Invoke(arg);
        }
    }

}
