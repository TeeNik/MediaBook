using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;

public class Constructor : MonoBehaviour
{
    //[SerializeField] private List<BookElement> Elements;
    private List<BookElement> _elements;
    [SerializeField] private Transform _bookRoot;

    public void Init()
    {
        _elements = new List<BookElement>();
        _elements.AddRange(Resources.LoadAll<BookElement>("Prefabs/"));
    }

    public void GenerateBook(XmlElement root, PageController controller)
    {
        foreach (XmlNode node in root)
        {
            controller.AddPage((PageElement)CreateItem(node, _bookRoot));
        }
    }

    private BookElement CreateItem(XmlNode node, Transform parent)
    {
        var prototype = _elements.Find(e => e.Type == node.Name);
        Assert.Inv(prototype != null, "prototype != null", prototype);
        var element = Instantiate(prototype, parent);
        element.Init(node);
        return element;
    }
}
