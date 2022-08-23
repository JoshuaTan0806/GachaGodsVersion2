using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HoldButton))]
public class ViewCharacterPreview : MonoBehaviour
{
    Character character;
    CharacterPreview preview;
    HoldButton button;

    private void Awake()
    {
        button = GetComponent<HoldButton>();
    }

    private void OnEnable()
    {
        button.OnHeld += PreviewCharacter;
    }

    private void OnDisable()
    {
        button.OnHeld -= PreviewCharacter;
    }

    public void Initialise(Character character)
    {
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
