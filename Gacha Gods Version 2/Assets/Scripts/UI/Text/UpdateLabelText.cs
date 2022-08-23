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
    RoundNumber,
    Stars,
    Wins,
    Health
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
            case TextType.Stars:
                GameManager.OnStarsChanged += UpdateText;
                break;
            case TextType.Wins:
                GameManager.OnBattleWon += UpdateText;
                break;
            case TextType.Health:
                GameManager.OnHealthChanged += UpdateText;
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
            case TextType.Stars:
                GameManager.OnStarsChanged -= UpdateText;
                break;
            case TextType.Wins:
                GameManager.OnBattleWon -= UpdateText;
                break;
            case TextType.Health:
                GameManager.OnHealthChanged -= UpdateText;
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
                label.SetText(GameManager.Gold.ToString());
                break;
            case TextType.Gems:
                label.SetText(GameManager.Gems.ToString());
                break;
            case TextType.XP:
                label.SetText(GameManager.Experience.ToString());
                break;
            case TextType.Level:
                label.SetText(GameManager.Level.ToString());
                break;
            case TextType.RoundNumber:
                label.SetText("Day " + GameManager.RoundNumber);
                break;
            case TextType.Stars:
                label.SetText(GameManager.Stars.ToString());
                break;
            case TextType.Wins:
                label.SetText(GameManager.Wins.ToString());
                break;
            case TextType.Health:
                label.SetText(GameManager.Health.ToString());
                break;
            default:
                break;
        }
    }
}
