﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class QuestionPanel : BookElement
    {
        public override string Type => ElementTag.QuestionPanel;

        [SerializeField] private TMPro.TMP_Text _question;
        [SerializeField] private Transform _container;
        [SerializeField] private ToggleGroup group;

        private List<AnswerToggle> _toggles;
        private string _answerId;

        public override void Init(XmlNode content)
        {
            var constructor = DataLayer.Instance.Constructor;
            _toggles = new List<AnswerToggle>();
            _answerId = content.Attributes["answer"].InnerText;

            foreach (XmlNode node in content)
            {
                AnswerToggle item = constructor.CreateItem(node, _container) as AnswerToggle;
                item.SetToggleGroup(group);
            }
        }

        public bool CheckAnswer()
        {
            return _toggles.First(t => t.GetToggleValue()).Id == _answerId;
        }
    }
}