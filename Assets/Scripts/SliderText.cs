using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SliderText : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(Change);
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void Change(float value)
    {
        _text.text = value.ToString();
    }
}