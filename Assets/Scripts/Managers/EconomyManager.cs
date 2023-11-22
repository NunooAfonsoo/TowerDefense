using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private EconomyConfig economyConfig;

    [SerializeField] private IntEventSO creepKilledEvent;
    [SerializeField] private IntEventSO turretPlacedEvent;
    [SerializeField] private IntEventSO moneyChangedEvent;

    private int money;

    private void Awake()
    {
        money = economyConfig.StartingMoney;
    }

    private void Start()
    {
        moneyChangedEvent.RaiseEvent(money);

        creepKilledEvent.OnEventRaised += UpdateMoney;
        turretPlacedEvent.OnEventRaised += UpdateMoney;
    }

    private void UpdateMoney(int income)
    {
        money += income;

        moneyChangedEvent.RaiseEvent(money);
    }
}
