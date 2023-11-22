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

        waveStartedEvent.OnEventRaised += ChangeCreepsRemaining;
        remainingCreepsChangedEvent.OnEventRaised += ChangeCreepsRemaining;
    }

    private void ChangeCreepsRemaining(int creepsRemaining)
    {
        this.creepsRemaining.text = "Creeps Remaining: " + creepsRemaining.ToString();
    }
}
