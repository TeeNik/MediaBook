using System;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class TurnButtonElement : BookElement
    {
        private const string Right = "-right";
        private const string Left = "-left";

        public override string Type => "turn_button" + (_isRight ? Right : Left);

        [SerializeField] private bool _isRight;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public override void Init(XmlNode content)
        {
            var hexColor = content.Attributes["color"].InnerText;
            var text = content.Attributes["text"].InnerText;

            _image.color = Utils.ParseColor(hexColor);
            _text.text = text;

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            var controller = DataLayer.Instance.PageController;
            if (_isRight)
            {
                controller.NextPage();
            }
            else
            {
                controller.PrevPage();
            }
        }
    }
}


