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

    [SerializeField] CharacterSelection characterSelection;

    [SerializeField] UnityEngine.UI.Image characterIcon;

    private void Awake()
    {
        gameObject.AddListenerToButton(() => ViewCharacters());
    }

    void ViewCharacters()
    {
        StartCoroutine(PickCharacter());
    }

    IEnumerator PickCharacter()
    {
        CharacterSelection g = Instantiate(characterSelection);
        g.Initialise(unitType);

        while (!g.HasChosen)
        {
            if (g == null)
                yield break;

            yield return new WaitForSeconds(0.1f);
        }

        PlaceCharacter(g.ChosenCharacter);
        Destroy(g.gameObject);
    }

    void RemoveCharacter()
    {
        characterIcon.gameObject.SetActive(false);
        characterIcon.sprite = null;
        character = null;
    }

    void PlaceCharacter(Character chosenCharacter)
    {
        if (chosenCharacter == null && character != null)
        {
            CharacterManager.DeactivateCharacter(character);
            RemoveCharacter();
            return;
        }

        //if the chosen character isn't on the field...
        if (!CharacterManager.ActiveCharacters.Contains(chosenCharacter))
        {
            //if theres a character on this spot..
            if (character != null)
            {
                //replace them
                CharacterManager.DeactivateCharacter(character);
            }
            else
            {
                //we can't add any more characters
                if (CharacterManager.ActiveCharacters.Count == GameManager.Level)
                    return;
            }

            //add them
            if (chosenCharacter != null)
                CharacterManager.ActivateCharacter(chosenCharacter);
        }
        //if the chosen character is on the field...
        else
        {
            //find the tile they are on
            Tile tile = null;

            foreach (var item in Board.Tiles)
            {
                if (item.character == chosenCharacter)
                    tile = item;
            }

            tile.RemoveCharacter();

            //if theres a character on this spot..
            if (character != null)
            {
                //swap them
                Character oldCharacter = character;
                RemoveCharacter();
                tile.PlaceCharacter(oldCharacter);
            }
        }

        character = chosenCharacter;
        characterIcon.gameObject.SetActive(true);

        if (chosenCharacter != null)
            characterIcon.sprite = chosenCharacter.Icon;
    }
}
