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
    public UnitType UnitType => unitType;
    UnitType unitType;
    public CharacterStats Character => character;
    CharacterStats character;

    [SerializeField] GameObject arrow;

    private void OnEnable()
    {
        BoardManager.OnHeldCharacterChanged += SetTransparency;
    }

    private void OnDisable()
    {
        BoardManager.OnHeldCharacterChanged -= SetTransparency;
    }

    void SetTransparency()
    {
        if (BoardManager.HeldCharacter == null)
            arrow.SetActive(false);
        else
        {
            if (CanBePlaced(BoardManager.HeldCharacter.Character))
                arrow.SetActive(true);
            else
                arrow.SetActive(false);
        }
    }

    public void SetUnitType(UnitType unitType)
    {
        this.unitType = unitType;
    }

    public bool CanBePlaced(Character character)
    {
        switch (unitType)
        {
            case UnitType.None:
                return false;
            case UnitType.All:
                return true;
            case UnitType.Assassin:
                return character.Roles.Contains(CharacterManager.Assassin);
            default:
                return false;
        }
    }

    public CharacterStats PickUpCharacter()
    {
        CharacterStats characterStats = Character;
        character = null;
        return characterStats;
    }

    public void PlaceCharacter(CharacterStats character)
    {
        this.character = character;
    }
}
