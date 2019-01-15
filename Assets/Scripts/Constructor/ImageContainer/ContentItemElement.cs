using System;
using System.Xml;
using Generator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class ContentItemElement : BookElement
    {
        public override string Type => "content_item";

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public override void Init(XmlNode content)
        {
            _text.text = content.InnerText;
            _button.onClick.AddListener(() =>
            {
                var page = content.Attributes["page"];
                Assert.Inv(page != null, "page != null");
                var num = Convert.ToInt32(page.InnerText);
                DataLayer.Instance.PageController.OpenPage(num);
            });
        }
    }

}


