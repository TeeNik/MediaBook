using System;

public class UnityBehavior<T> : IDisposable where T : IComparable
{
    public T CurrentValue { get; private set; }
    public T PrevValue { get; private set; }

    #region Implementation of IUnitySubject<T>

    public void OnNext(T value)
    {
        if (CurrentValue == null && value == null) return;
        if (CurrentValue != null && CurrentValue.CompareTo(value) == 0) return;

        PrevValue = CurrentValue;
        CurrentValue = value;

        Handler?.Invoke(CurrentValue);
        DiffHandler?.Invoke(CurrentValue, PrevValue);
    }


    public void AlwaysChange(T value)
    {
        PrevValue = CurrentValue;
        CurrentValue = value;

        Handler?.Invoke(CurrentValue);
        DiffHandler?.Invoke(CurrentValue, PrevValue);
    }

    public IDisposable Subscribe(Action<T> action, bool callOnInit = true)
    {
        if (Handler != null &&
            Array.Find(Handler.GetInvocationList(), d => d.Equals(action)) != null)
        {
            throw new ArgumentException("action added twice");
        }
        if (callOnInit)
        {
            action(CurrentValue);
        }
        Handler += action;
        return new AnonymousDisposable(() => { Handler -= action; });
    }

    public IDisposable Subscribe(Action<T, T> action)
    {
        if (DiffHandler != null &&
            Array.Find(DiffHandler.GetInvocationList(), d => d.Equals(action)) != null)
        {
            throw new ArgumentException("action added twice");
        }
        DiffHandler += action;
        return new AnonymousDisposable(() => { DiffHandler -= action; });
    }

    #endregion

    #region Implementation of IDisposable

    public void Dispose()
    {
        Handler = null;
        DiffHandler = null;
    }

    #endregion

    public UnityBehavior(T @default)
    {
        CurrentValue = @default;
    }

    public override string ToString()
    {
        return CurrentValue.ToString();
    }

    private event Action<T> Handler;
    private event Action<T, T> DiffHandler;
}
