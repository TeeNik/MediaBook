using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFader : MonoBehaviour
{
    public bool IsHideOnStart;
    public CanvasGroup Canvas;

    void Start()
    {
        if (IsHideOnStart)
        {
            SetAlpha(0);
        }
    }

    public void FadeTo(float endValue, float time, Action onComplete = null)
    {
        Canvas.DOFade(endValue, time).OnComplete(()=>
        {
            onComplete?.Invoke();
        });
    }

    public void Fade(float from, float to, float time, Action onComplete = null)
    {
        SetAlpha(from);
        FadeTo(to, time, onComplete);
    }

    public void SetAlpha(float value)
    {
        Canvas.alpha = value;
    }

    public float Alpha => Canvas.alpha;
}
