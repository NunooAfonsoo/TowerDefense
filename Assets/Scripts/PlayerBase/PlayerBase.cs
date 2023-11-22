using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : Singleton<PlayerBase>, IDamageable
{
    public int HitPoints { get; private set; }

    [SerializeField] private PlayerBaseConfig playerBaseConfig;

    [field: SerializeField] public Slider HealthBar { get; private set; }

    [SerializeField] private VoidEventSO gameLostEvent;

    protected override void Awake()
    {
        base.Awake();

        HitPoints = playerBaseConfig.HP;
        HealthBar.maxValue = HitPoints;
        HealthBar.value = HitPoints;
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        HealthBar.value = HitPoints;

        if (HitPoints == 0)
        {
            gameLostEvent.RaiseEvent();
        }
    }
}
