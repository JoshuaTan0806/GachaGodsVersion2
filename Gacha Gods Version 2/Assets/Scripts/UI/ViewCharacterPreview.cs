using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ViewCharacterPreview : MonoBehaviour
{
    Character character;
    CharacterPreview preview;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Initialise(Character character)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => PreviewCharacter());

        this.character = character;
        GetComponent<Image>().sprite = character.Icon;
    }

    void PreviewCharacter()
    {
        if (character == null)
            return;

        preview = Instantiate(UIManager.instance.CharacterPreviewPrefab);
        preview.Initialise(character);
    }
}
