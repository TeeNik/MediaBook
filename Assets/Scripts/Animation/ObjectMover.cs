using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public RectTransform _rect;
    private Tweener _tweener;

    void Start()
    {
        if(_rect == null)
            _rect = GetComponent<RectTransform>();
    }

    public void MoveTo(Vector2 to, float time)
    {
        KillPlaying();
        _tweener = _rect.DOAnchorPos(to, time);
    }

    public void SetPosition(Vector2 pos)
    {
        KillPlaying();
        _rect.anchoredPosition = pos;
    }

    public void Move(Vector2 from, Vector2 to, float time)
    {
        SetPosition(from);
        MoveTo(to, time);
    }

    public void MoveByX(float fromX, float toX, float time)
    {
        SetPosition(new Vector2(fromX, _rect.anchoredPosition.y));
        _tweener =  _rect.DOAnchorPosX(toX, time);
    }

    public Vector2 Position => _rect.anchoredPosition;

    private void KillPlaying()
    {
        if (_tweener != null && _tweener.IsPlaying())
        {
            _tweener.Kill();
        }
    }

}
