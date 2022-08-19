using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None,
    All,
    Assassin
}

public class Tile : MonoBehaviour
{
    [SerializeField] UnitType unitType;
    public Character Character => character;
    Character character;

    public Character PickUpCharacter()
    {
        Character character = this.character;
        this.character = null;
        return character;
    }

    public void PlaceCharacter(Character character)
    {
        this.character = character;
    }
}
