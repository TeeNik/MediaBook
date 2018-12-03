using UnityEngine;

namespace Generator
{
    public abstract class BookElement : MonoBehaviour
    {

        public string Type;

        public abstract void Init(string content);

    }

}


