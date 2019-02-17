using UIWindow;
using UnityEngine.EventSystems;
public class ModelViewWindow : Window, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
    }
}
