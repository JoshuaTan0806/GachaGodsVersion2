using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Character/Attack")]
public class AbilityData : ScriptableObject
{
    public Ability Prefab => prefab;
    [SerializeField] Ability prefab;

    public string Description => description;
    [SerializeField] string description;

    public float ActionSpeed => actionSpeed;
    [Min(0), SerializeField] float actionSpeed = 1;

    public int Range => range;
    [Min(0), SerializeField] int range = 1;

    public int NumberOfTargets => numberOfTargets;
    [Min(0), SerializeField] int numberOfTargets = 1;

    public int NumberOfSpawns => numberOfSpawns;
    [Min(0), SerializeField] int numberOfSpawns = 1;

    public TeamType TeamType => teamType;
    [SerializeField] TeamType teamType = TeamType.Enemy;

    public AllyTargetType AllyTargetType => allyTargetType;
    [SerializeField, ShowIf("TeamType", TeamType.Ally)] AllyTargetType allyTargetType;

    public TargetType TargetType => targetType;
    [SerializeField] TargetType targetType = TargetType.Current;

    public Stat HighestStat => highestStat;
    [SerializeField, ShowIf("TargetType", TargetType.HighestStat)] Stat highestStat;

    public Stat LowestStat => lowestStat;
    [SerializeField, ShowIf("TargetType", TargetType.LowestStat)] Stat lowestStat;

    [Button] 
    void SetAsAttack()
    {
        range = 1;
        numberOfTargets = 1;
        numberOfSpawns = 1;
        teamType = TeamType.Enemy;
        targetType = TargetType.Current;
    }

    [Button]
    void DestroyScriptable()
    {
        DestroyImmediate(this, true);
    }
}

public enum TeamType
{
    Ally,
    Enemy
}

public enum TargetType
{
    Current,
    Closest,
    Furthest,
    HighestStat,
    LowestStat,
    HighestDensity
}

public enum AbilityType
{
    SpawnDirectlyOnTarget,
    Projectile
}

public enum AllyTargetType
{
    Self,
    PrioritiseOthers,
    Others
}
