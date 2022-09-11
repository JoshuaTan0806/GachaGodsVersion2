using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Attack")]
public class AbilityData : ScriptableObject
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;

    public string Description => description;
    [SerializeField] string description;

    public int MaxRange => maxRange;
    [SerializeField] int maxRange = 1;

    public int NumberOfTargets => numberOfTargets;
    [SerializeField] int numberOfTargets = 1;

    public int NumberOfSpawns => numberOfSpawns;
    [SerializeField] int numberOfSpawns = 1;

    public TargetType TargetType => targetType;
    [SerializeField] TargetType targetType;
}

public enum TargetType
{
    CurrentEnemy,
    ClosestEnemy,
    FurthestEnemy,
    HighestHealthEnemy,
    LowestHealthEnemy,
}

public enum AbilityType
{
    SpawnDirectlyOnTarget,
    Projectile
}
