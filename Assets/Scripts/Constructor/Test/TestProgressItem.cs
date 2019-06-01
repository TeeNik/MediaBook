using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class TestProgressItem : MonoBehaviour
    {
        [SerializeField] private Image _circle;
        [SerializeField] private Image _border;
        [SerializeField] private TMP_Text _number;

        public void Init(int num)
        {
            _number.text = num.ToString();
            //_circle.color = UIColor.Orange;
        }

        public void SetColor(Color color)
        {
            _circle.color = color;
        }
    }
}


