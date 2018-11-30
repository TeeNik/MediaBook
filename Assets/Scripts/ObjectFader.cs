using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFader : MonoBehaviour
{
    public bool IsHideOnStart;
    public CanvasGroup _canvas;

    public void FadeTo(float endValue, float time, Action OnComplete = null)
    {
        _canvas.DOFade(endValue, time).OnComplete(()=>
        {
            OnComplete?.Invoke();
        });
    }

    public void Fade(float from, float to, float time, Action OnComplete = null)
    {
        SetAlpha(from);
        FadeTo(to, time, OnComplete);
    }

    public void SetAlpha(float value)
    {
        _canvas.alpha = value;
    }

    public float Alpha => _canvas.alpha;
}
