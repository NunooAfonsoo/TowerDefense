using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Creep : MonoBehaviour, IDamageable, ISlowable
{
    [SerializeField] protected CreepConfig creepConfig;

    public int HitPoints { get; private set; }
    [field: SerializeField] public Slider HealthBar { get; private set; }

    [SerializeField] private IntEventSO creepKilledEvent;

    private float currentSpeed;
    private Vector3 playerBasePosition;

    private void Awake()
    {
        playerBasePosition = PlayerBase.Instance.transform.position;

        currentSpeed = creepConfig.Speed;
        HitPoints = creepConfig.HP;
        HealthBar.maxValue = HitPoints;
        HealthBar.value = HitPoints;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.LookAt(playerBasePosition);

        transform.position = Vector3.MoveTowards(transform.position, playerBasePosition, currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == Constants.TERRAIN_NAME)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out IDamageable damageable) & !(damageable is Creep))
        {
            damageable.TakeDamage(creepConfig.Damage);

            creepKilledEvent.RaiseEvent(0);

            Destroy(gameObject);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        HitPoints -= damage;
        HealthBar.value = HitPoints;

        if (HitPoints <= 0)
        {
            creepKilledEvent.RaiseEvent(creepConfig.Income);

            Destroy(gameObject);
        }
    }

    public IEnumerator SlowDown(float time)
    {
        float slowDownMultiplier = 0.5f;
        currentSpeed = creepConfig.Speed * slowDownMultiplier;
        
        yield return new WaitForSeconds(time);

        currentSpeed = creepConfig.Speed;
    }
}
