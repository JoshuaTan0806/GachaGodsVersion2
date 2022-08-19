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
    public int Position => position;
    int position;

    public CharacterData(Character character, int mastery, int position)
    {
        this.character = character;
        this.mastery = mastery;
        this.position = position;
    }
}
