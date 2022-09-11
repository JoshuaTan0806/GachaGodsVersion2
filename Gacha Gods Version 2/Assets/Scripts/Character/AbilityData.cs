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

    public bool HasMaxRange => hasMaxRange;
    [SerializeField] bool hasMaxRange = false;

    public int MaxRange => maxRange;
    [SerializeField, ShowIf("hasMaxRange", true)] int maxRange = 1;

    public int NumberOfTargets => numberOfTargets;
    [SerializeField] int numberOfTargets = 1;

    public int NumberOfSpawns => numberOfSpawns;
    [SerializeField] int numberOfSpawns = 1;

    public TeamType TeamType => teamType;
    [SerializeField] TeamType teamType;

    public TargetType TargetType => targetType;
    [SerializeField] TargetType targetType;

    [Button] 
    void SetAsAttack()
    {
        maxRange = 1;
        numberOfTargets = 1;
        numberOfSpawns = 1;
        hasMaxRange = false;
        teamType = TeamType.Enemy;
        targetType = TargetType.Current;
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
    HighestHealth,
    LowestHealth,
    HighestDensity
}

public enum AbilityType
{
    SpawnDirectlyOnTarget,
    Projectile
}
