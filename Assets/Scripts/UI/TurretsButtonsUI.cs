using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretsButtonsUI : MonoBehaviour
{
    [SerializeField] private TurretConfig normalTurretConfig;
    [SerializeField] private TurretConfig freezeTurretConfig;

    [SerializeField] private Button normalTurretButton;
    [SerializeField] private Button freezeTurretButton;

    [SerializeField] private GameObject normalTurret;
    [SerializeField] private GameObject freezeTurret;

    [SerializeField] private IntEventSO moneyChangedEvent;
    [SerializeField] private VoidEventSO gameWonEvent;
    [SerializeField] private VoidEventSO gameLostEvent;


    private void Awake()
    {
        normalTurretButton.onClick.AddListener(() => SpawnTurret(normalTurret, normalTurretConfig.Cost));
        normalTurretButton.GetComponentInChildren<TextMeshProUGUI>().text = "Normal Turret (" + normalTurretConfig.Cost + "$)";

        freezeTurretButton.onClick.AddListener(() => SpawnTurret(freezeTurret, freezeTurretConfig.Cost));
        freezeTurretButton.GetComponentInChildren<TextMeshProUGUI>().text = "Freeze Turret (" + freezeTurretConfig.Cost + "$)";
    }

    private void Start()
    {
        moneyChangedEvent.OnEventRaised += CheckButtonsInteractability;
        gameWonEvent.OnEventRaised += DisableButtons;
        gameLostEvent.OnEventRaised += DisableButtons;
    }

    private void SpawnTurret(GameObject turret, int cost)
    {
        ITurret newTurret = Instantiate(turret).GetComponent<ITurret>();
        CursorManager.Instance.SetPlaceableTurret(newTurret, cost);
    }

    private void CheckButtonsInteractability(int money)
    {
        normalTurretButton.interactable = money >= normalTurretConfig.Cost;
        freezeTurretButton.interactable = money >= freezeTurretConfig.Cost;
    }

    /// <summary>
    /// Buttons get disabled after game over to not allow player to add more towers
    /// </summary>
    private void DisableButtons()
    {
        moneyChangedEvent.OnEventRaised -= CheckButtonsInteractability;

        normalTurretButton.interactable = false;
        freezeTurretButton.interactable = false;
    }
}
