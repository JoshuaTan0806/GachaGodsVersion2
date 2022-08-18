using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public int RoundNumber => roundNumber;
    int roundNumber;
    public List<CharacterData> CharacterDatas => characterDatas;
    List<CharacterData> characterDatas;
    public ActiveRoles Roles => roles;
    ActiveRoles roles;
    public ActiveArchetypes Archetypes => archetypes;
    ActiveArchetypes archetypes;

    public BoardData(int roundNumber, List<CharacterData> characterDatas, ActiveRoles roles, ActiveArchetypes archetypes)
    {
        this.roundNumber = roundNumber;
        this.roles = roles;
        this.archetypes = archetypes;
        this.characterDatas = characterDatas;
    }
}

public class CharacterData
{
    public Character Character => character;
    Character character;
    public int Mastery => mastery;
    int mastery;
    public Attack Attack => attack;
    Attack attack;
    public Spell Spell => spell;
    Spell spell;
    public StatDictionary Stats => stats;
    StatDictionary stats;
    public Vector2Int Position => position;
    Vector2Int position;

    public CharacterData(Character character, int mastery, Attack attack, Spell spell, StatDictionary stats, Vector2Int position)
    {
        this.character = character;
        this.mastery = mastery;
        this.attack = attack;
        this.spell = spell;
        this.stats = stats;
        this.position = position;
    }
}
