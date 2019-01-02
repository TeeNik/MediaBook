using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;

public class Constructor : MonoBehaviour
{
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
            var page = (PageElement)CreateItem(node, _bookRoot);
            page.gameObject.SetActive(false);
            controller.AddPage(page);
        }
        controller.OpenPage(0);
    }

    public BookElement CreateItem(XmlNode node, Transform parent)
    {
        var prototype = _elements.Find(e => e.Type == node.Name);
        Assert.Inv(prototype != null, "prototype != null", node.Name);
        var element = Instantiate(prototype, parent);
        element.Init(node);
        return element;
    }
}
