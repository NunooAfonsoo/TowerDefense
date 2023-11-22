using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] protected ProjectileConfig projectileConfig;

    private Vector3 targetDirection;

    private void Awake()
    {
        Invoke(nameof(Destroy), Constants.TIME_TO_PROJECTILE_TO_SELF_DESTROY);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(targetDirection * projectileConfig.Speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 targetPosition)
    {
        targetDirection = targetPosition - transform.position;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == Constants.TERRAIN_NAME)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(projectileConfig.Damage);

            Destroy(gameObject);
        }
    }
}