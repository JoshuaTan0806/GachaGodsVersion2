using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum MasteryType
{
    Trait,
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

    public List<Trait> Traits => traits;
    [ShowIf("MasteryType", MasteryType.Trait), SerializeField]
    List<Trait> traits;

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
