using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private IntEventSO waveNumberChanged;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(ValueChangeCheck);
    }

    private void ValueChangeCheck(float value)
    {
        waveNumberChanged.RaiseEvent((int)value);
    }
}
