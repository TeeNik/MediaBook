using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class TestProgressItem : BookElement
    {
        [SerializeField] private Image _circle;
        [SerializeField] private Image _border;
        [SerializeField] private TMP_Text _number;

        public override string Type => ElementTag.TestProgressItem;
        public override void Init(XmlNode content)
        {
         
            

        }
    }
}


