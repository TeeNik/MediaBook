using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthWindow : MonoBehaviour
{

    public TMP_InputField NameInput;
    public TMP_InputField GroupInput;
    public Button SubmitButton;

    void Start()
    {
        SubmitButton.onClick.AddListener(SubmitAuth);
    }

    private void SubmitAuth()
    {
        var name = NameInput.text;
        var group = GroupInput.text;
        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(group))
        {
            DataLayer.Instance.AuthController.Auth(name, group);
        }
    }
}
