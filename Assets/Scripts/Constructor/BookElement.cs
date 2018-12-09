using UnityEngine;

namespace Generator
{
    public abstract class BookElement : MonoBehaviour
    {
        protected abstract string Type { get; }

        public abstract void Init(string content);

    }

}


