using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum StatRequirement
{
    None,
    Archetype,
    Role
}

[CreateAssetMenu(menuName = "Character/Stat")]
public class StatData : ScriptableObject
{
    public Stat Stat => stat;
    [SerializeField] Stat stat;

    public StatRequirement StatRequirement => statRequirement;
    [SerializeField] StatRequirement statRequirement = StatRequirement.None;

    public Archetype Archetype => archetype;
    [ShowIf("statRequirement", StatRequirement.Archetype), SerializeField] Archetype archetype;
    public Role Role => role;
    [ShowIf("statRequirement", StatRequirement.Role), SerializeField] Role role;

    public float Flat
    {
        get
        {
            return flat;
        }
        set
        {
            flat = value;
            CalculateTotal();
        }
    }
    [SerializeField] float flat = 1;

    public float Percent
    {
        get
        {
            return percent;
        }
        set
        {
            percent = value;
            CalculateTotal();
        }
    }
    [SerializeField] float percent;

    public float Multiplier
    {
        get
        {
            return multiplier;
        }
        set
        {
            multiplier = value;
            CalculateTotal();
        }
    }
    [SerializeField]  float multiplier = 1;

    public float Total => total;
    [ReadOnly, SerializeField] float total;

    private void OnValidate()
    {
        CalculateTotal();
    }

    public void CalculateTotal()
    {
        total = ((flat + (flat * (percent / 100))) * multiplier);
    }

    public static StatData operator+ (StatData l, StatData r)
    {
        if (l.Stat != r.Stat)
            throw new System.Exception("Stats are different");

        StatData stat = StatManager.NullStat(l.Stat);

        stat.Flat = l.Flat + r.Flat;
        stat.Percent = l.Percent + r.Percent;
        stat.Multiplier = l.Multiplier * r.Multiplier;

        return stat;
    }

    public static StatData operator - (StatData l, StatData r)
    {
        if (l.Stat != r.Stat)
            throw new System.Exception("Stats are different");

        StatData stat = StatManager.NullStat(l.Stat);

        stat.Flat = l.Flat - r.Flat;
        stat.Percent = l.Percent - r.Percent;
        stat.Multiplier = l.Multiplier * 1 / r.Multiplier;

        return stat;
    }
}

[System.Serializable] public class StatDictionary : SerializableDictionary<Stat, StatData> { }
[System.Serializable] public class StatFloatDictionary : SerializableDictionary<Stat, float> { }

public enum Stat
{
    Health,
    Range,
    Dmg,
    ManaCost,
    StartingMana,
    ManaGainPerHit,
    AtkSpd,
    SpellSpd,
    SpellDmg,
    Spd,
    CritChance,
    CritMult,
    Lifesteal
}

public enum StatModifier
{
    Flat,
    Percent,
    Multiplier
}