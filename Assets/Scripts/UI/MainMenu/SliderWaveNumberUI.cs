using TMPro;
using UnityEngine;

public class SliderWaveNumberUI : MonoBehaviour
{
    [SerializeField] private IntEventSO waveNumberChangedEvent;

    private TextMeshProUGUI waveNumber;

    private void Awake()
    {
        waveNumber = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        waveNumberChangedEvent.OnEventRaised += ChangeWaveNumber;
    }

    private void ChangeWaveNumber(int waveNumber)
    {
        this.waveNumber.text = waveNumber.ToString();
    }
}
