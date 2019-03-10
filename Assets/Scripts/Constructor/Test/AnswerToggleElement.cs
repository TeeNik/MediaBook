using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class AnswerToggleElement : BookElement
    {
        public override string Type => ElementTag.AnswerToggle;
        public string Id { get; private set; }

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Toggle _toggle;

        public override void Init(XmlNode content)
        {
            Id = content.Attributes["id"].InnerText;
            _text.text = content.InnerText;
        }

        public void SetToggleGroup(ToggleGroup group)
        {
            _toggle.group = group;
        }

        public bool GetToggleValue()
        {
            return _toggle.isOn;
        }
    }
}