using UnityEngine;

[CreateAssetMenu(fileName = "CreepConfig", menuName = "ScriptableObjects/Configs/CreepConfigSO")]
public class CreepConfig : BaseScriptableObject
{
    public int HP;
    public float Speed;
    public int Damage;
    public int Income;
}