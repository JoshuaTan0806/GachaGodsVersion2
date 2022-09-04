using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public int RoundNumber => roundNumber;
    int roundNumber;
    public List<CharacterData> CharacterDatas => characterDatas;
    List<CharacterData> characterDatas;
    public ActiveTraits Traits => traits;
    ActiveTraits traits;

    public BoardData(int roundNumber, List<CharacterData> characterDatas, ActiveTraits traits)
    {
        this.roundNumber = roundNumber;
        this.traits = traits;
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
