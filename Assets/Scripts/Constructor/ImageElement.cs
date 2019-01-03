using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class ImageElement : BookElement
    {
        public override string Type => "image_item";

        [SerializeField] private Image _image;

        public override void Init(XmlNode content)
        {
            var src = content.Attributes["src"];
            Assert.Inv(src != null, "src != null");

            string spriteName = src.InnerText;
            var sprite = DataLayer.Instance.BookResources.Get<Sprite>(spriteName);
            _image.sprite = sprite;
        }
    }

}

