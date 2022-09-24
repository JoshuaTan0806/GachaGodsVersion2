using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public enum MasteryType
{
    Trait,
    Stat,
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

    public List<AbilityData> Attacks => attacks;
    [ShowIf("MasteryType", MasteryType.Attack), SerializeField]
    List<AbilityData> attacks;

    public List<AbilityData> Spells => spells;
    [ShowIf("MasteryType", MasteryType.Spell), SerializeField]
    List<AbilityData> spells;

    [Button]
    void Refresh()
    {
        OnValidate();
    }

    [ShowIf("MasteryType", MasteryType.Stat), Button]
    void CreateStatData()
    {
        StatData stats = ScriptableObject.CreateInstance<StatData>();
        stats.name = name + " S";
        this.stats.Add(stats);
        AssetDatabase.AddObjectToAsset(stats, this);
    }

    [ShowIf("MasteryType", MasteryType.Attack), Button]
    void CreateAttack()
    {
        AbilityData attack = ScriptableObject.CreateInstance<AbilityData>();
        attack.name = name + " Attack " + (attacks.Count + 1);
        attacks.Add(attack);
        AssetDatabase.AddObjectToAsset(attack, this);

        OnValidate();
    }

    [ShowIf("MasteryType", MasteryType.Spell), Button]
    void CreateSpell()
    {
        AbilityData spell = ScriptableObject.CreateInstance<AbilityData>();
        spell.name = name + " Spell " + (attacks.Count + 1);
        spells.Add(spell);
        AssetDatabase.AddObjectToAsset(spell, this);

        OnValidate();
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

    private void OnValidate()
    {
        for (int i = attacks.Count - 1; i >= 0; i--)
        {
            if (attacks[i] == null)
                attacks.RemoveAt(i);
        }

        for (int i = 0; i < attacks.Count; i++)
        {
            attacks[i].name = name + " Attack " + (i + 1);
        }

        for (int i = spells.Count - 1; i >= 0; i--)
        {
            if (spells[i] == null)
                spells.RemoveAt(i);
        }

        for (int i = 0; i < spells.Count; i++)
        {
            spells[i].name = name + " Spell " + (i + 1);
        }

        foreach (var item in stats)
        {
            item.name = name + " " + item.Stat.ToString();
        }
    }
}
