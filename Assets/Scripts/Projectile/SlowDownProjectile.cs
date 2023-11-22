using UnityEngine;

public class SlowDownProjectile : Projectile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == Constants.TERRAIN_NAME)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out ISlowable creep))
        {
            SlowDownProjectileConfig slowDownProjectileConfig = (SlowDownProjectileConfig)projectileConfig;

            StartCoroutine(creep.SlowDown(slowDownProjectileConfig.SlowDownTime));
        }

        base.OnCollisionEnter(collision);
    }
}
