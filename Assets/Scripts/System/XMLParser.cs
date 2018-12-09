using System.Xml;
using UnityEngine;

public class XMLParser {

    public void GetNodes(TextAsset asset)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(asset.text);
        XmlElement root = doc.DocumentElement;
        foreach (XmlNode node in root)
        {
            Debug.Log(node);
        }
    }

}
