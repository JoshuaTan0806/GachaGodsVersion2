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

    void InitialiseCharacter(Character chosenCharacter)
    {
        characterIcon.gameObject.SetActive(true);
        characterIcon.sprite = chosenCharacter.Icon;
        CharacterManager.ActivateCharacter(chosenCharacter);
        character = chosenCharacter;
    }

    void RemoveCharacter()
    {
        characterIcon.gameObject.SetActive(false);
        characterIcon.sprite = null;
        CharacterManager.DeactivateCharacter(character);
        character = null;
    }

    void PlaceCharacter(Character chosenCharacter)
    {
        if (character != null)
            RemoveCharacter();

        foreach (var item in Board.Tiles)
        {
            if (item.character == chosenCharacter)
            {
                item.RemoveCharacter();
            }
        }

        if (CharacterManager.ActiveCharacters.Count == GameManager.Level)
            return;

        InitialiseCharacter(chosenCharacter);
    }
}
