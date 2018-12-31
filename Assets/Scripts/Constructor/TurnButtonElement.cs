using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class TurnButtonElement : BookElement
    {
        public override string Type => "turn_button" + _property;

        [SerializeField] private string _property;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public override void Init(XmlNode content)
        {
            var hexColor = content.Attributes["color"].InnerText;
            var text = content.Attributes["text"].InnerText;

            _image.color = Utils.ParseColor(hexColor);
            _text.text = text;
        }
    }
}


