using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu(menuName = "Managers/Character Manager")]
public class CharacterManager : Factories.FactoryBase
{
    public static List<Character> Characters => characters;
    static List<Character> characters = new List<Character>();
    [SerializeField] List<Character> _characters;

    public static List<Role> Roles => roles;
    static List<Role> roles = new List<Role>();
    [SerializeField] List<Role> _roles;

    public static Role Assassin => assassin;
    static Role assassin;
    [SerializeField] Role _assassin;


    public static List<Archetype> Archetypes => archetypes;
    static List<Archetype> archetypes = new List<Archetype>();
    [SerializeField] List<Archetype> _archetypes;

    public static List<Rarity> Rarities => rarities;
    static List<Rarity> rarities = new List<Rarity>();
    [SerializeField] List<Rarity> _rarities;

    public static List<OddsDictionary> Odds => odds;
    static List<OddsDictionary> odds = new List<OddsDictionary>();
    List<int> _odds = new List<int>()
    {
        100, 0, 0, 0, 0,
        100, 0, 0, 0, 0,
        100, 0, 0, 0, 0,
        75, 25, 0, 0, 0,
        55, 30, 15, 0, 0,
        45, 33, 20, 2, 0,
        25, 40, 30, 5, 0,
        19, 30, 35, 15, 1,
        16, 20, 35, 25, 4,
        9, 15, 30, 30, 16,
        5, 10, 20, 40, 25
    };

    public static CharacterMastery CharacterMastery => characterMastery;
    static CharacterMastery characterMastery = new CharacterMastery();
    public static ActiveCharacters ActiveCharacters => activeCharacters;
    static ActiveCharacters activeCharacters = new ActiveCharacters();
    public static ActiveRoles ActiveRoles => activeRoles;
    static ActiveRoles activeRoles = new ActiveRoles();
    public static ActiveArchetypes ActiveArchetypes => activeArchetypes;
    static ActiveArchetypes activeArchetypes = new ActiveArchetypes();
    public static List<StatData> GlobalBuffs => globalBuffs;
    static List<StatData> globalBuffs = new List<StatData>();
    public static GameObject HeldCharacter;
    public static System.Action<Character> OnCharacterPulled;

    public override void Initialise()
    {
        GameManager.OnGameStart -= Clear;
        GameManager.OnGameStart += Clear;

        GameManager.OnGameEnd -= Clear;
        GameManager.OnGameEnd += Clear;

        assassin = _assassin;
        characters = _characters;
        roles = _roles;
        archetypes = _archetypes;
        rarities = _rarities.OrderBy(x => x.RarityNumber).ToList();
        InitialiseOdds();
    }

    public void InitialiseOdds()
    {
        odds.Clear();
        int counter = 0;

        for (int i = 0; i < 11; i++)
        {
            odds.Add(new OddsDictionary());

            for (int j = 0; j < _rarities.Count; j++)
            {
                odds[i].Add(_rarities[j], _odds[counter]);
                counter++;
            }
        }
    }

    void Clear()
    {
        CharacterMastery.Clear();
        ActiveCharacters.Clear();
        ActiveRoles.Clear();
        ActiveArchetypes.Clear();
        GlobalBuffs.Clear();
    }

    public static void AddCharacter(Character character)
    {
        if (CharacterMastery.ContainsKey(character))
        {
            if (CharacterMastery[character] < CharacterMastery.Count)
                CharacterMastery[character]++;

            if (ActiveCharacters.ContainsKey(character))
                character.Mastery[CharacterMastery[character]].ActivateMastery(ActiveCharacters[character]);
        }
        else
        {
            CharacterMastery.Add(character, 0);
        }

        OnCharacterPulled?.Invoke(character);
    }

    public static void ActivateCharacter(CharacterStats characterStats)
    {
        Character character = characterStats.Character;
        characterStats.UpgradeAttack(character.Attack);
        characterStats.UpgradeSpell(character.Spell);

        if (activeCharacters.ContainsKey(character))
            throw new System.Exception("Can't add a character which is already active");
        else
        {
            AddAllGlobalBuffs(characterStats);

            activeCharacters.Add(character, characterStats);

            for (int i = 0; i < character.Roles.Count; i++)
            {
                AddRole(character.Roles[i]);
            }

            for (int i = 0; i < character.Archetypes.Count; i++)
            {
                AddArchetype(character.Archetypes[i]);
            }

            for (int i = 0; i < CharacterMastery[character]; i++)
            {
                character.Mastery[i].ActivateMastery(characterStats);
            }
        }
    }

    public static void DeactivateCharacter(Character character)
    {
        if (!activeCharacters.ContainsKey(character))
            throw new System.Exception("Can't remove a character that is inactive");
        else
        {
            for (int i = 0; i < CharacterMastery[character]; i++)
            {
                character.Mastery[i].DeactiveMastery(activeCharacters[character]);
            }

            for (int i = 0; i < character.Roles.Count; i++)
            {
                RemoveRole(character.Roles[i]);
            }
    
            for (int i = 0; i < character.Archetypes.Count; i++)
            {
                RemoveArchetype(character.Archetypes[i]);
            }

            activeCharacters.Remove(character);
        }
    }
    
    public static void AddRole(Role role)
    {
        if (!ActiveRoles.ContainsKey(role))
            ActiveRoles.Add(role, 1);
        else
            ActiveRoles[role]++;

        if (role.SetData.ContainsKey(ActiveRoles[role]))
            AddGlobalBuff(role.SetData[ActiveRoles[role]]);
    }

    public static void AddArchetype(Archetype archetype)
    {
        if (!ActiveArchetypes.ContainsKey(archetype))
            ActiveArchetypes.Add(archetype, 1);
        else
            ActiveArchetypes[archetype]++;

        if (archetype.SetData.ContainsKey(ActiveArchetypes[archetype]))
            AddGlobalBuff(archetype.SetData[ActiveArchetypes[archetype]]);
    }

    public static void RemoveRole(Role role)
    {
        if (role.SetData.ContainsKey(ActiveRoles[role]))
            RemoveGlobalBuff(role.SetData[activeRoles[role]]);

        if (!ActiveRoles.ContainsKey(role))
            throw new System.Exception("Trying to remove a role not in the dictionary");
        else
            ActiveRoles[role]--;

        if (ActiveRoles[role] == 0)
            ActiveRoles.Remove(role);
    }

    public static void RemoveArchetype(Archetype archetype)
    {
        if (archetype.SetData.ContainsKey(ActiveArchetypes[archetype]))
            RemoveGlobalBuff(archetype.SetData[ActiveArchetypes[archetype]]);

        if (!ActiveArchetypes.ContainsKey(archetype))
            throw new System.Exception("Trying to remove a archetype not in the dictionary");
        else
            ActiveArchetypes[archetype]--;

        if (ActiveArchetypes[archetype] == 0)
            ActiveArchetypes.Remove(archetype);
    }

    public static void AddGlobalBuff(StatData statData)
    {
        if (globalBuffs.Contains(statData))
            throw new System.Exception("Trying to add a buff that has already been added");
        else
            globalBuffs.Add(statData);

        foreach (var item in ActiveCharacters)
        {
            item.Value.AddStat(statData);
        }
    }

    public static void RemoveGlobalBuff(StatData statData)
    {
        if (!globalBuffs.Contains(statData))
            throw new System.Exception("Trying to remove a buff that has not been added");
        else
            globalBuffs.Remove(statData);

        foreach (var item in ActiveCharacters)
        {
            item.Value.RemoveStat(statData);
        }
    }

    public static void AddAllGlobalBuffs(CharacterStats characterStats)
    {
        foreach (var item in globalBuffs)
        {
            characterStats.AddStat(item);
        }
    }
}

public class ActiveCharacters : SerializableDictionary<Character, CharacterStats> { }
public class CharacterMastery : SerializableDictionary<Character, int> { }
public class ActiveRoles : SerializableDictionary<Role, int> { }
public class ActiveArchetypes : SerializableDictionary<Archetype, int> { }
[System.Serializable] public class SetData : SerializableDictionary<int, StatData> { }
[System.Serializable] public class OddsDictionary : SerializableDictionary<Rarity, int> { }