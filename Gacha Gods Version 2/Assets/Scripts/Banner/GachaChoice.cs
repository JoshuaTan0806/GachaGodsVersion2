using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaChoice : MonoBehaviour
{
    [SerializeField]
    List<UnityEngine.UI.Button> buttons;
    [SerializeField]
    List<GameObject> rateUps;
    public bool HasBeenPicked => hasBeenPicked;
    bool hasBeenPicked;
    public void Initialise(Dictionary<Character, bool> characters)
    {
        hasBeenPicked = false;

        foreach (var item in rateUps)
            item.SetActive(false);

        int counter = 0;

        foreach (var item in characters)
        {
            Character character = item.Key;

            if (item.Value)
                rateUps[counter].gameObject.SetActive(true);

            buttons[counter].onClick.RemoveAllListeners();

            string str = character.name + "\n" + character.Rarity;

            foreach (var archetype in character.Archetypes)
            {
                str += "\n" + archetype.name;
            }

            foreach (var role in character.Roles)
            {
                str += "\n" + role.name;
            }

            buttons[counter].GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(str);

            buttons[counter].onClick.AddListener(delegate { AddCharacter(character); });

            counter++;
        }
    }

    void AddCharacter(Character character)
    {
        if (hasBeenPicked)
            return;

        CharacterManager.AddCharacter(character);
        hasBeenPicked = true;
    }
}
