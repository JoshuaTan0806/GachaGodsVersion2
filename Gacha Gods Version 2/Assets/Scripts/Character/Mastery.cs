using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum MasteryType
{
    Role,
    Archetype,
    Stat,
    GlobalStat,
    Attack,
    Spell
}

[CreateAssetMenu(menuName = "Character/Mastery")]
public class Mastery : ScriptableObject
{
    public string Description => description;
    [SerializeField] string description;

    public MasteryType MasteryType => masteryType;
    [SerializeField] MasteryType masteryType;

    public List<Role> Roles => roles;
    [ShowIf("MasteryType", MasteryType.Role), SerializeField]
    List<Role> roles;

    public List<Archetype> Archetypes => archetypes;
    [ShowIf("MasteryType", MasteryType.Archetype), SerializeField]
    List<Archetype> archetypes;

    public List<StatData> Stats => stats;
    [ShowIf("MasteryType", MasteryType.Stat), SerializeField]
    List<StatData> stats;

    public List<StatData> GlobalStats => globalStats;
    [ShowIf("MasteryType", MasteryType.GlobalStat), SerializeField]
    List<StatData> globalStats;

    public Attack Attack => attack;
    [ShowIf("MasteryType", MasteryType.Attack), SerializeField]
    Attack attack;

    public Spell Spell => spell;
    [ShowIf("MasteryType", MasteryType.Spell), SerializeField]
    Spell spell;
}
