using System.Xml;
using TMPro;

namespace Generator
{
    public class TextElement : BookElement
    {
        public override string Type => "Text";

        public TMP_Text Text;

        public override void Init(XmlNode content)
        {
            Text.text = content.InnerText;
        }
    }
}


