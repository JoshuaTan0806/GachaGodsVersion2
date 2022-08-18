using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaChoice : MonoBehaviour
{
    [SerializeField]
    List<UnityEngine.UI.Button> buttons;
    public bool HasBeenPicked => hasBeenPicked;
    bool hasBeenPicked;

    public void Initialise(List<Character> characters)
    {
        hasBeenPicked = false;

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(characters[i].name + " " + characters[i].Rarity);

            Character character = characters[i];
            buttons[i].onClick.AddListener(delegate { AddCharacter(character); });
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
