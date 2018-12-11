using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;

namespace Generator
{
    public class TextElement : BookElement
    {
        protected override string Type => "Text";

        public override void Init(XmlNodeList content)
        {
            
        }
    }
}


