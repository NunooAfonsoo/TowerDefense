using TMPro;
using UnityEngine;

public class CreepsRemainingUI : MonoBehaviour
{
    [SerializeField] private IntEventSO waveStartedEvent;
    [SerializeField] private IntEventSO remainingCreepsChangedEvent;

    private TextMeshProUGUI creepsRemaining;

    private void Awake()
    {
        creepsRemaining = GetComponent<TextMeshProUGUI>();

        waveStartedEvent.OnEventRaised += UpdateCreepsRemaining;
        remainingCreepsChangedEvent.OnEventRaised += UpdateCreepsRemaining;
    }

    private void UpdateCreepsRemaining(int creepsRemaining)
    {
        this.creepsRemaining.text = "Creeps Remaining: " + creepsRemaining.ToString();
    }
}
