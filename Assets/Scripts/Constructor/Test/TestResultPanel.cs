using DG.Tweening;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestResultPanel : MonoBehaviour
{

    [SerializeField] private float _time = 5;
    [SerializeField] private Image _resultImage;
    [SerializeField] private TMP_Text _resultText;

    public void Show(int correct, int all)
    {
        gameObject.SetActive(true);
        float end = (float)correct / all;

        _resultText.text = $"{correct}/{all}";
        _resultImage.fillAmount = 0;
        _resultImage.DOFillAmount(end, _time);
        float color = 0;
        float endColor = 125 * end;
        DOTween.To(() => color, (x) =>
        {
            color = x;
            _resultImage.color = Color.HSVToRGB(color/360, 1, .64f);
        }, endColor, _time);
    }
}
