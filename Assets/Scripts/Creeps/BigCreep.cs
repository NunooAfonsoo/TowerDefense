using UnityEngine;

public class BigCreep : Creep
{
    public override void TakeDamage(int damageToTake)
    {
        float probability = Random.Range(0f, 1f);
        BigCreepConfig bigCreepConfig = (BigCreepConfig)creepConfig;

        if (probability < bigCreepConfig.BigCreepAbsortionProbability) // Creep absorbs the projectile without taking damage
        {
            return;
        }

        base.TakeDamage(damageToTake);
    }
}
