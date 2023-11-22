using TMPro;
using UnityEngine;

public class WaveNumberUI : MonoBehaviour
{
    [SerializeField] private IntEventSO waveEndedEvent;

    private TextMeshProUGUI waveNumber;

    private void Awake()
    {
        waveNumber = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        waveEndedEvent.OnEventRaised += ChangeWaveNumber;
    }

    private void ChangeWaveNumber(int waveNumber)
    {
        this.waveNumber.text = "Wave " + waveNumber.ToString();
    }
}
