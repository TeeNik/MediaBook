 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 [AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class CameraController : MonoBehaviour
{
    public float speed = 8;
    public float distance = 3;
    public Transform target;
    private Vector2 input;

    public void Update()
    {
        input += new Vector2(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed);
        transform.localRotation = Quaternion.Euler(input.y, input.x, 0);
        transform.localPosition = target.position - (transform.localRotation * Vector3.forward * distance);
    }
}