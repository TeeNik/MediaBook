using System.Xml;
using Generator;
using TMPro;

public class PageElement : BookElement
{
    public override string Type => "page";

    public TMP_Text Title;

    public override void Init(XmlNode content)
    {
        /*var constructor = DataLayer.Instance.Constructor;
        foreach (XmlNode node in content)
        {
            constructor.CreateItem(node, transform);
        }*/
        var title = content.Attributes["title"].InnerText;
        Title.text = title;
    }
}
