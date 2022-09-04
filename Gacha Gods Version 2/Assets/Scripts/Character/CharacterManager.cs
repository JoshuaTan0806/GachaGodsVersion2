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

    public static List<Trait> Traits => traits;
    static List<Trait> traits = new List<Trait>();
    [SerializeField] List<Trait> _traits;

    public static Trait Assassin => assassin;
    static Trait assassin;
    [SerializeField] Trait _assassin;

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
    public static ActiveTraits ActiveTraits
    {
        get
        {
            ActiveTraits trait = new ActiveTraits();

            foreach (var character in activeCharacters)
            {
                foreach (var role in character.Traits)
                {
                    if (trait.ContainsKey(role))
                        trait[role]++;
                    else
                        trait.Add(role, 1);
                }

                for (int i = 0; i < characterMastery[character]; i++)
                {
                    if(character.Mastery[i].MasteryType == MasteryType.Trait)
                    {
                        foreach (var role in character.Mastery[i].Traits)
                        {
                            if (trait.ContainsKey(role))
                                trait[role]++;
                            else
                                trait.Add(role, 1);
                        }
                    }
                }
            }

            return trait;
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
        traits = _traits;
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
        ActiveTraits.Clear();
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

    public static List<StatData> FindStatsFromTraits(ActiveTraits traits)
    {
        List<StatData> statDatas = new List<StatData>();

        foreach (var item in traits)
        {
            StatDatas stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats.Stats)
            {
                statDatas.Add(stat);
            }
        }

        return statDatas;
    }
}

public class CharacterMastery : SerializableDictionary<Character, int> { }
public class ActiveTraits : SerializableDictionary<Trait, int> { }
[System.Serializable] public class SetData : SerializableDictionary<int, StatDatas> { }
[System.Serializable] public class OddsDictionary : SerializableDictionary<Rarity, int> { }