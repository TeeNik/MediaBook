using System.Xml;
using UnityEngine;

namespace Generator
{
    public abstract class BookElement : MonoBehaviour
    {
        public abstract string Type { get; }

        public abstract void Init(XmlNodeList content);

    }

}


