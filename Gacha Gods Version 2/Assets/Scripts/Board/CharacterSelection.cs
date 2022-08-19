using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public bool HasChosen => hasChosen;
    bool hasChosen;

    public Character ChosenCharacter => chosenCharacter;
    Character chosenCharacter;
}
