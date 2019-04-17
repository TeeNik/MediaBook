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

        [SerializeField] private Transform _container;
        private List<QuestionPanel> _questions;
        private List<TestProgressItem> _progressItems;

        private const string _progressItemTag = "<test_progress_item color>{}</test_progress_item>";

        public override void Init(XmlNode content)
        {
            var constructor = DataLayer.Instance.Constructor;
            _questions = new List<QuestionPanel>();

            foreach (XmlNode node in content)
            {
                QuestionPanel item = constructor.CreateItem(node, _container) as QuestionPanel;
                _questions.Add(item);
            }
        }


        private void OnProgressItemClick()
        {

        }
    }
}