using UnityEngine;

public class SlowDownProjectile : Projectile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ISlowable creep))
        {
            SlowDownProjectileConfig slowDownProjectileConfig = (SlowDownProjectileConfig)projectileConfig;

            StartCoroutine(creep.SlowDown(slowDownProjectileConfig.SlowDownTime));

            Destroy(gameObject);
        }

        base.OnCollisionEnter(collision);
    }
}
