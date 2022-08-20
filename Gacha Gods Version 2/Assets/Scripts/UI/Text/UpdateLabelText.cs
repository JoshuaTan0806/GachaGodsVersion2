using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TextType
{
    Gold,
    Gems,
    XP,
    Level,
    RoundNumber
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateLabelText : MonoBehaviour
{
    [SerializeField] TextType textType;
    TextMeshProUGUI label;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();    
    }

    private void OnEnable()
    {
        UpdateText();

        switch (textType)
        {
            case TextType.Gold:
                GameManager.OnGoldChanged += UpdateText;
                break;
            case TextType.Gems:
                GameManager.OnGemsChanged += UpdateText;
                break;
            case TextType.XP:
                GameManager.OnExperienceChanged += UpdateText;
                break;
            case TextType.Level:
                GameManager.OnLevelChanged += UpdateText;
                break;
            case TextType.RoundNumber:
                GameManager.OnRoundStart += UpdateText;
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        switch (textType)
        {
            case TextType.Gold:
                GameManager.OnGoldChanged -= UpdateText;
                break;
            case TextType.Gems:
                GameManager.OnGemsChanged -= UpdateText;
                break;
            case TextType.XP:
                GameManager.OnExperienceChanged -= UpdateText;
                break;
            case TextType.Level:
                GameManager.OnLevelChanged -= UpdateText;
                break;
            case TextType.RoundNumber:
                GameManager.OnRoundStart -= UpdateText;
                break;
            default:
                break;
        }
    }

    void UpdateText()
    {
        switch (textType)
        {
            case TextType.Gold:
                label.SetText("Gold: " + GameManager.Gold);
                break;
            case TextType.Gems:
                label.SetText("Gems: " + GameManager.Gems);
                break;
            case TextType.XP:
                label.SetText("XP: " + GameManager.Experience);
                break;
            case TextType.Level:
                label.SetText("Level: " + GameManager.Level);
                break;
            case TextType.RoundNumber:
                label.SetText("RoundNumber: " + GameManager.RoundNumber);
                break;
            default:
                break;
        }
    }
}
