using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class ImageContainerElement : BookElement
    {
        public override string Type => ElementTag.ImageContainer;

        [SerializeField] private Transform _content;
        [SerializeField] private Button _next;
        [SerializeField] private Button _prev;

        private List<GameObject> _list;
        private int _current;

        public override void Init(XmlNode content)
        {
            _list = new List<GameObject>();
            var constructor = DataLayer.Instance.Constructor;

            foreach (XmlNode node in content)
            {
                var item = constructor.CreateItem(node, _content);
                item.gameObject.SetActive(false);
                _list.Add(item.gameObject);
            }
            _list.First().gameObject.SetActive(true);
            _next.onClick.AddListener(NextImage);
            _prev.onClick.AddListener(PrevImage);
        }

        public void NextImage()
        {
            OpenPage(_current + 1);
        }

        public void PrevImage()
        {
            OpenPage(_current - 1);
        }

        public void OpenPage(int number)
        {
            int count = _list.Count;
            if (number >= count)
            {
                number = 0;
            }
            else if (number < 0)
            {
                number = count - 1;
            }

            _list[_current].gameObject.SetActive(false);
            _current = number;
            _list[_current].gameObject.SetActive(true);
        }
    }

}


