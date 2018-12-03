using System;
using DG.Tweening;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{

    public RectTransform Rect;

    public void ScaleTo(Vector3 to, float time, Action onComplete = null)
    {
        Rect.DOScale(to, time).OnComplete(()=>onComplete?.Invoke());
    }

    public void Scale(Vector3 from, Vector3 to, float time, Action onComplete = null)
    {
        Rect.localScale = from;
        Rect.DOScale(to, time).OnComplete(() => onComplete?.Invoke());
    }

}
