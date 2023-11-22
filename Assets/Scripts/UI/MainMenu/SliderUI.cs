using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private IntEventSO waveNumberChanged;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(float value)
    {
        waveNumberChanged.RaiseEvent((int)value);
    }
}
