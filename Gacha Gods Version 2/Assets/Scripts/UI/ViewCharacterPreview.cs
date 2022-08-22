using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewCharacterPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Character character;
    GameObject preview;

    public void Initialise(Character character)
    {
        this.character = character;
        GetComponent<Image>().sprite = character.Icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (character == null)
            return;

        //preview = Instantiate(UIManager.instance.CharacterPreviewPrefab);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Destroy(preview);
    }
}
