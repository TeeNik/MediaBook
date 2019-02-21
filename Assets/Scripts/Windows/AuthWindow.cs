using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthWindow : MonoBehaviour
{

    public InputField NameInput;
    public InputField GroupInput;
    public Button SubmitButton;

    void Start()
    {
        SubmitButton.onClick.AddListener(SubmitAuth);
    }

    private void SubmitAuth()
    {
        DataLayer.Instance.        
    }
}
