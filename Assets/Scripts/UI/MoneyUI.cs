using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private IntEventSO moneyChangedUIEvent;

    private TextMeshProUGUI money;

    private void Awake()
    {
        money = GetComponent<TextMeshProUGUI>();

        moneyChangedUIEvent.OnEventRaised += UpdateMoneyText;
    }

    private void UpdateMoneyText(int money)
    {
        this.money.text = money + " $";
    }
}
