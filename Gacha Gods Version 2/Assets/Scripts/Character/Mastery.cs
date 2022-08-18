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

    [ShowIf("MasteryType", MasteryType.Role), SerializeField]
    List<Role> Roles;

    [ShowIf("MasteryType", MasteryType.Archetype), SerializeField]
    List<Archetype> Archetypes;

    [ShowIf("MasteryType", MasteryType.Stat), SerializeField]
    List<StatData> Stats;

    [ShowIf("MasteryType", MasteryType.GlobalStat), SerializeField]
    List<StatData> GlobalStats;

    [ShowIf("MasteryType", MasteryType.Attack), SerializeField]
    Attack Attack;

    [ShowIf("MasteryType", MasteryType.Spell), SerializeField]
    Spell Spell;


    public void ActivateMastery(CharacterStats stats)
    {
        switch (MasteryType)
        {
            case MasteryType.Role:
                foreach (var item in Roles)
                {
                    CharacterManager.AddRole(item);
                }
                break;
            case MasteryType.Archetype:
                foreach (var item in Archetypes)
                {
                    CharacterManager.AddArchetype(item);
                }
                break;
            case MasteryType.Stat:
                foreach (var item in Stats)
                {
                    stats.AddStat(item);
                }
                break;
            case MasteryType.GlobalStat:
                foreach (var item in GlobalStats)
                {
                    CharacterManager.AddGlobalBuff(item);
                }
                break;
            case MasteryType.Attack:
                stats.UpgradeAttack(Attack);
                break;
            case MasteryType.Spell:
                stats.UpgradeSpell(Spell);
                break;
            default:
                break;
        }
    }

    public void DeactiveMastery(CharacterStats stats)
    {
        switch (MasteryType)
        {
            case MasteryType.Role:
                foreach (var item in Roles)
                {
                    CharacterManager.RemoveRole(item);
                }
                break;
            case MasteryType.Archetype:
                foreach (var item in Archetypes)
                {
                    CharacterManager.RemoveArchetype(item);
                }
                break;
            case MasteryType.Stat:
                foreach (var item in Stats)
                {
                    stats.RemoveStat(item);
                }
                break;
            case MasteryType.GlobalStat:
                foreach (var item in GlobalStats)
                {
                    CharacterManager.RemoveGlobalBuff(item);
                }
                break;
            case MasteryType.Attack:
                break;
            case MasteryType.Spell:
            default:
                break;
        }
    }
}
