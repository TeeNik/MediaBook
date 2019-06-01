using System;
using UnityEngine;

public class Utils
{

    public static Color ParseColor(string color)
    {
        Color newColor = Color.clear;
        ColorUtility.TryParseHtmlString(color, out newColor);
        return newColor;
    }

    public static void DisposeAndSetNull<T>(ref T some) where T : class
    {
        var disposable = some as IDisposable;
        if (disposable != null)
        {
            disposable.Dispose();
            some = null;
        }
    }
}
