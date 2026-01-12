using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class CustomSlider : MonoBehaviour
{
    [SerializeField] Slider _slider;

    float _startPosX;
    float _endPosX;

    public event UnityAction<float> OnValueChange;
    public event UnityAction<float> OnPositionChange;

    void Start()
    {
        _slider.onValueChanged.AddListener(TriggerEvents);

        var layout = GetComponent<HorizontalLayoutGroup>();       
        _startPosX = layout.padding.left;;
        _endPosX = Screen.width - layout.padding.right;
        OnPositionChange?.Invoke(_startPosX);
    }

    public void ResetValue()
    {
        _slider.value = 0;
    }

    public void TriggerEvents(float value)
    {
        OnValueChange?.Invoke(value);
        float posX = Mathf.Lerp(_startPosX, _endPosX, value);
        OnPositionChange?.Invoke(posX);
    }
}
