using UnityEngine;

[CreateAssetMenu(fileName = "TurretConfig", menuName = "ScriptableObjects/Configs/TurretConfigSO")]
public class TurretConfig : BaseScriptableObject
{
    public int Cost;
    public float ScanRadius;
    public float TimeBetweemShots;
}