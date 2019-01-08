using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class ImageElement : BookElement
    {
        public override string Type => "image_item";

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        private string _spriteName;

        public override void Init(XmlNode content)
        {
            var src = content.Attributes["src"];
            Assert.Inv(src != null, "src != null");

            _spriteName = src.InnerText;
            var sprite = DataLayer.Instance.BookResources.Get<Sprite>(_spriteName);
            _image.sprite = sprite;
            _button.onClick.AddListener(OpenView);
        }

        private void OpenView()
        {
            DataLayer.Instance.Messages.OnNext(new OpenImageViewMsg{spriteName = _spriteName});
        }
    }

}

