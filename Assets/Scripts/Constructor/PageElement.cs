﻿using System.Xml;
using Generator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageElement : BookElement
{
    public override string Type => "page";

    public TMP_Text Title;
    public Image Image;
    public string Id { get; private set; }

    public override void Init(XmlNode content)
    {
        var title = content.Attributes["title"].InnerText;
        Title.text = title;
        var constructor = DataLayer.Instance.Constructor;

        var id = content.Attributes["id"];
        Assert.Inv(id != null, "id != null");
        Id = id.InnerText;

        if (content.Attributes["image"] != null)
        {
            var imageSrc = content.Attributes["image"].InnerText;
            var image = DataLayer.Instance.BookResources.Get<Sprite>(imageSrc);
            Image.sprite = image;
        }

        foreach (XmlNode node in content)
        {
            constructor.CreateItem(node, transform);
        }
    }
}
