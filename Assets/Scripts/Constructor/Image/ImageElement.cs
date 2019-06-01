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
        private bool _isModel;
        private GameObject _object;

        public override void Init(XmlNode content)
        {
            var src = content.Attributes["src"];
            Assert.Inv(src != null, "src != null");

            _spriteName = src.InnerText;
            var sprite = DataLayer.Instance.BookResources.Get<Sprite>(_spriteName);
            _image.sprite = sprite;
            _button.onClick.AddListener(OpenView);

            if (content.Attributes["is_model"] != null && content.Attributes["is_model"].InnerText == "true")
            {
                _isModel = true;
                var modelSrc = content.Attributes["model_src"];
                Assert.Inv(modelSrc != null, "modelSrc != null");
                _object = DataLayer.Instance.BookResources.Get<GameObject>(modelSrc.InnerText);
                Assert.Inv(_object != null, "_object != null");

            }
        }

        private void OpenView()
        {
            if (_isModel)
            {
                DataLayer.Instance.Messages.OnNext(new OpemModelViewMsg { go = _object });
            }
            else
            {
                DataLayer.Instance.Messages.OnNext(new OpenImageViewMsg { spriteName = _spriteName });
            }
        }
    }
}