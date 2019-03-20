using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;

namespace Generator
{
    public class TestBlock : BookElement
    {
        public override string Type => ElementTag.TestBlock;

        public override void Init(XmlNode content)
        {

        }
    }
}

