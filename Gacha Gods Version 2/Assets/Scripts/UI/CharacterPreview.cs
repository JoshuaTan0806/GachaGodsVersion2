using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPreview : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] List<TextMeshProUGUI> traits;
    [SerializeField] TextMeshProUGUI masteryNumber;

    public void Initialise(Character character)
    {
        characterName.text = character.name;
        icon.sprite = character.Icon;

        int counter = 0;

        foreach (var item in character.Traits)
        {
            traits[counter].gameObject.SetActive(true);
            traits[counter].text = item.name;
            counter++;
        }

        masteryNumber.text = CharacterManager.CharacterMastery.ContainsKey(character) ? CharacterManager.CharacterMastery[character].ToString() : "N/A";
    }
}
