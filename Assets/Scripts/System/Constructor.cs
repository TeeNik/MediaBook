using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;

public class Constructor : MonoBehaviour
{

    public List<BookElement> Elements;

    public void CreateItem(XmlNode node, Transform parent)
    {
        var prototype = Elements.Find(e => e.Type == node.Name);
        Assert.Inv(prototype != null, "prototype != null", prototype);
        var element = Instantiate(prototype, parent);
        element.Init(node.ChildNodes);
    }

}
