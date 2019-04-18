using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Generator;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class TestBlock : BookElement
    {
        public override string Type => ElementTag.TestBlock;

        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _progressItem;
        [SerializeField] private Button _answerButton;
        private List<QuestionPanel> _questions;
        private List<TestProgressItem> _progressItems;

        private int _currentQuestion;
        private int _correct;

        public override void Init(XmlNode content)
        {
            var constructor = DataLayer.Instance.Constructor;
            _questions = new List<QuestionPanel>();

            foreach (XmlNode node in content)
            {
                QuestionPanel item = constructor.CreateItem(node, _container) as QuestionPanel;
                _questions.Add(item);
            }

            _answerButton.onClick.AddListener(Answer);
        }

        private void OnProgressItemClick()
        {

        }

        private void SetAnswerButtonEnable(bool value)
        {
            _answerButton.image.color = value ? UIColor.Green : UIColor.Gray;
            _answerButton.interactable = value;
        }

        private void Answer()
        {
            if (_questions[_currentQuestion].CheckAnswer())
            {
                _correct++;
            }

            if (_currentQuestion == _progressItems.Count)
            {
                DataLayer.Instance.PageController.NextPage();
            }
            else
            {
                SetAnswerButtonEnable(false);
                _progressItems[_currentQuestion].gameObject.SetActive(false);
                ++_currentQuestion;
                _progressItems[_currentQuestion].gameObject.SetActive(true);
            }
        }
    }
}