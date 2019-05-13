using UIWindow;
using UnityEngine;
using UnityEngine.EventSystems;
public class ModelViewWindow : Window, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform Object;
    public Transform Container;

    private float _sensitivity = 0.5f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
    }

    float rotSpeed = 300;
    public void OnDrag(PointerEventData eventData)
    {
        print("drag");
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        Object.Rotate(rotY, -rotX, 0, Space.World);
        //Object.Rotate(Vector3.right, rotY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        print("end drag");
    }
}
