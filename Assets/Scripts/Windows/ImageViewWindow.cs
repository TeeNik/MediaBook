using UIWindow;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewWindow : Window
{
    [SerializeField] private Image _image;

    public override void Init()
    {
        base.Init();
        var messages = DataLayer.Instance.Messages;
        _subscriptions.Add(messages.Subscribe<OpenImageViewMsg>(OnOpenViewMsg));
    }

    private void OnOpenViewMsg(OpenImageViewMsg msg)
    {
        var sprite = DataLayer.Instance.BookResources.Get<Sprite>(msg.spriteName);
        _image.sprite = sprite;
        OpenWindow();
    }
}
