using System.Xml;
using Generator;

public class PageElement : BookElement
{
    public override string Type => "Page";

    public override void Init(XmlNodeList content)
    {
        var constructor = DataLayer.Instance.Constructor;
        foreach (XmlNode node in content)
        {
            constructor.CreateItem(node, transform);
        }
    }
}
