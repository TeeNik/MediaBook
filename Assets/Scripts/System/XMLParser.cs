using System.Xml;
using UnityEngine;

public class XMLParser {

    public XmlElement GetRoot(TextAsset asset)
    {
        XmlDocument doc = new XmlDocument();
        doc.PreserveWhitespace = false;
        doc.LoadXml(asset.text);
        XmlElement root = doc.DocumentElement;
        return root;
    }

}
