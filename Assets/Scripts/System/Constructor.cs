using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;

public class Constructor : MonoBehaviour
{
    [SerializeField] private List<BookElement> Elements;
    [SerializeField] private Transform BookRoot;

    public void GenerateBook(XmlElement root, PageController controller)
    {
        foreach (XmlNode node in root)
        {
            controller.AddPage((PageElement)CreateItem(node, BookRoot));
        }
    }

    private BookElement CreateItem(XmlNode node, Transform parent)
    {
        var prototype = Elements.Find(e => e.Type == node.Name);
        Assert.Inv(prototype != null, "prototype != null", prototype);
        var element = Instantiate(prototype, parent);
        element.Init(node);
        return element;
    }
}
