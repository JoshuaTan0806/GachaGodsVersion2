using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

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
    [ShowIf("MasteryType", MasteryType.Stat), SerializeField, ReadOnly]
    List<StatData> stats;

    public List<StatData> GlobalStats => globalStats;
    [ShowIf("MasteryType", MasteryType.GlobalStat), SerializeField, ReadOnly]
    List<StatData> globalStats;

    public Ability Attack => attack;
    [ShowIf("MasteryType", MasteryType.Attack), SerializeField]
    Ability attack;

    public Ability Spell => spell;
    [ShowIf("MasteryType", MasteryType.Spell), SerializeField]
    Ability spell;

    [ShowIf("MasteryType", MasteryType.Stat), Button]
    void RefreshStatNames()
    {
        foreach (var item in stats)
        {
            item.name = name + " " + item.Stat.ToString();
        }
    }

    [ShowIf("MasteryType", MasteryType.GlobalStat), Button]
    void RefreshGlobalStatNames()
    {
        foreach (var item in globalStats)
        {
            item.name = name + " " + item.Stat.ToString();
        }
    }

    [ShowIf("MasteryType", MasteryType.Stat), Button]
    void CreateStatData()
    {
        StatData stats = ScriptableObject.CreateInstance<StatData>();
        stats.name = name + " S";
        this.stats.Add(stats);
        AssetDatabase.AddObjectToAsset(stats, this);
    }

    [ShowIf("MasteryType", MasteryType.GlobalStat), Button]
    void CreateGlobalStatData()
    {
        StatData stats = ScriptableObject.CreateInstance<StatData>();
        stats.name = name + " S";
        this.globalStats.Add(stats);
        AssetDatabase.AddObjectToAsset(stats, this);
    }

    [ShowIf("MasteryType", MasteryType.Stat), Button]
    void ClearStatData()
    {
        foreach (var item in stats)
        {
            DestroyImmediate(item, true);
        }

        stats.Clear();
    }

    [ShowIf("MasteryType", MasteryType.GlobalStat), Button]
    void ClearGlobalStatData()
    {
        foreach (var item in globalStats)
        {
            DestroyImmediate(item, true);
        }

        globalStats.Clear();
    }
}
