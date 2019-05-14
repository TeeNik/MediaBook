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
        [SerializeField] private TestProgressItem _progressPrefab;
        [SerializeField] private Transform _progressContainer;
        [SerializeField] private Button _answerButton;
        [SerializeField] private TestResultPanel _resultPanel;
        private List<QuestionPanel> _questions;
        private List<TestProgressItem> _progressItems;

        private int _currentQuestion;
        private int _correct;

        public override void Init(XmlNode content)
        {
            var constructor = DataLayer.Instance.Constructor;
            _questions = new List<QuestionPanel>();
            _progressItems = new List<TestProgressItem>();

            int num = 0;
            foreach (XmlNode node in content)
            {
                QuestionPanel item = constructor.CreateItem(node, _container) as QuestionPanel;
                _questions.Add(item);

                var progressItem = Instantiate(_progressPrefab, _progressContainer);
                progressItem.Init(++num);
                _progressItems.Add(progressItem);
            }

            _answerButton.onClick.AddListener(Answer);
        }

        private void SetAnswerButtonEnable(bool value)
        {
            _answerButton.image.color = value ? UIColor.Green : UIColor.Gray;
            _answerButton.interactable = value;
        }

        private void Answer()
        {
            if (!_questions[_currentQuestion].IsAnyToggleOn)
            {
                return;
            }

            if (_questions[_currentQuestion].CheckAnswer())
            {
                _correct++;
            }

            if (_currentQuestion == _questions.Count -1)
            {
                _questions[_currentQuestion].gameObject.SetActive(false);
                _resultPanel.Show(_correct, _questions.Count);
            }
            else
            {
                SetAnswerButtonEnable(false);
                _questions[_currentQuestion].gameObject.SetActive(false);
                ++_currentQuestion;
                _questions[_currentQuestion].gameObject.SetActive(true);
            }
        }
    }
}