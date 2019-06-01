using System.Xml;
using UnityEngine;

namespace Generator
{
    public class TemplateElement : BookElement
    {
        [SerializeField] private string _type;
        public override string Type => _type;
        public override void Init(XmlNode content)
        {
            var constructor = DataLayer.Instance.Constructor;
            foreach (XmlNode node in content)
            {
                constructor.CreateItem(node, transform);
            }
        }
    }

}


