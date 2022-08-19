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
    public static List<Character> ActiveCharacters => activeCharacters;
    static List<Character> activeCharacters = new List<Character>();
    public static ActiveRoles ActiveRoles
    {
        get
        {
            ActiveRoles roles = new ActiveRoles();

            foreach (var character in activeCharacters)
            {
                foreach (var role in character.Roles)
                {
                    if (roles.ContainsKey(role))
                        roles[role]++;
                    else
                        roles.Add(role, 1);
                }

                for (int i = 0; i < characterMastery[character]; i++)
                {
                    if(character.Mastery[i].MasteryType == MasteryType.Role)
                    {
                        foreach (var role in character.Mastery[i].Roles)
                        {
                            if (roles.ContainsKey(role))
                                roles[role]++;
                            else
                                roles.Add(role, 1);
                        }
                    }
                }
            }

            return roles;
        }
    }

    public static ActiveArchetypes ActiveArchetypes
    {
        get
        {
            ActiveArchetypes archetypes = new ActiveArchetypes();

            foreach (var character in activeCharacters)
            {
                foreach (var archetype in character.Archetypes)
                {
                    if (archetypes.ContainsKey(archetype))
                        archetypes[archetype]++;
                    else
                        archetypes.Add(archetype, 1);
                }

                for (int i = 0; i < characterMastery[character]; i++)
                {
                    if (character.Mastery[i].MasteryType == MasteryType.Role)
                    {
                        foreach (var archetype in character.Mastery[i].Archetypes)
                        {
                            if (archetypes.ContainsKey(archetype))
                                archetypes[archetype]++;
                            else
                                archetypes.Add(archetype, 1);
                        }
                    }
                }
            }

            return archetypes;
        }
    }
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
    }

    public static void AddCharacter(Character character)
    {
        if (CharacterMastery.ContainsKey(character))
        {
            if (CharacterMastery[character] < CharacterMastery.Count)
                CharacterMastery[character]++;
        }
        else
        {
            CharacterMastery.Add(character, 0);
        }

        OnCharacterPulled?.Invoke(character);
    }

    public static void ActivateCharacter(Character character)
    {
        if (activeCharacters.Contains(character))
            throw new System.Exception("Can't add a character which is already active");
        else
        {
            activeCharacters.Add(character);
        }
    }

    public static void DeactivateCharacter(Character character)
    {
        if (!activeCharacters.Contains(character))
            throw new System.Exception("Can't remove a character that is inactive");
        else
        {
            activeCharacters.Remove(character);
        }
    }
}

public class CharacterMastery : SerializableDictionary<Character, int> { }
public class ActiveRoles : SerializableDictionary<Role, int> { }
public class ActiveArchetypes : SerializableDictionary<Archetype, int> { }
[System.Serializable] public class SetData : SerializableDictionary<int, StatData> { }
[System.Serializable] public class OddsDictionary : SerializableDictionary<Rarity, int> { }