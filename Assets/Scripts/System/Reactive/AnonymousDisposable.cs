using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class AnonymousDisposable : IDisposable
{
    public static IDisposable Noop = new AnonymousDisposable(() => { });
    private Action _dispose;
    private bool _isDisposed;

    public AnonymousDisposable(Action dispose)
    {
        if (dispose == null)
        {
            throw new ArgumentNullException("dispose");
        }
        _dispose = dispose;
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        _dispose();
        _dispose = null;
    }
}
