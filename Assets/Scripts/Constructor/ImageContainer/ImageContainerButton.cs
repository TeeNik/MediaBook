using UnityEngine;
using UnityEngine.EventSystems;

public class ImageContainerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ObjectFader _fader;
    [SerializeField] private float _speed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _fader.FadeTo(1, _speed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _fader.FadeTo(0, _speed);
    }
}
