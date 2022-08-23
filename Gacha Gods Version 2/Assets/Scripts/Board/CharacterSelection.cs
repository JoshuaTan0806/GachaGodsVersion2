using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public bool HasChosen => hasChosen;
    bool hasChosen;

    public Character ChosenCharacter => chosenCharacter;
    Character chosenCharacter;

   [SerializeField] Button characterPrefab;
   [SerializeField] Transform characterHolder;

    //used for previewing every owned character
    public void InitialiseAll()
    {
        foreach (var item in CharacterManager.CharacterMastery)
        {
            Button b = Instantiate(characterPrefab, characterHolder);
            b.GetComponent<ViewCharacterPreview>().Initialise(item.Key);
        }
    }

    //used for selecting a character to add to your team
    public void Initialise(UnitType unitType)
    {
        if (unitType == UnitType.None)
            Destroy(gameObject);

        hasChosen = false;

        Button b = Instantiate(characterPrefab, characterHolder);
        b.onClick.AddListener(delegate { ChooseCharacter(null); });

        foreach (var item in CharacterManager.CharacterMastery)
        {
            if (unitType == UnitType.All)
            {
                b = Instantiate(characterPrefab, characterHolder);
                b.GetComponent<ViewCharacterPreview>().Initialise(item.Key);
                b.onClick.AddListener(delegate { ChooseCharacter(item.Key); });
            }
            else if(unitType == UnitType.Assassin)
            {
                if (!item.Key.Roles.Contains(CharacterManager.Assassin))
                    continue;

                b = Instantiate(characterPrefab, characterHolder);
                b.GetComponent<ViewCharacterPreview>().Initialise(item.Key);
                b.onClick.AddListener(delegate { ChooseCharacter(item.Key); });
            }
        }
    }
    
    void ChooseCharacter(Character character)
    {
        chosenCharacter = character;
        hasChosen = true;
    }
}
