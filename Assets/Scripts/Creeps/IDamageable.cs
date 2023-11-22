using UnityEngine.UI;

public interface IDamageable
{
    int HitPoints { get; }
    Slider HealthBar { get; }
    void TakeDamage(int damage);
}
