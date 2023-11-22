using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ScriptableObjects/Configs/ProjectileConfigSO")]
public class ProjectileConfig : BaseScriptableObject
{
    public float Speed;
    public int Damage;
}
