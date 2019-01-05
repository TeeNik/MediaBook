using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageViewWindow : MonoBehaviour
{

    void Start()
    {
        var messages = DataLayer.Instance.Messages;
        messages.Subscribe<OpenImageViewMsg>(OnOpenViewMsg);
    }

    private void OnOpenViewMsg(OpenImageViewMsg msg)
    {
        
    }
}
