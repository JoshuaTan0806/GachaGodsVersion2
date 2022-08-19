using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public enum BannerType
{
    Regular,
    RateUp,
}

public class Banner : MonoBehaviour
{
    public BannerType BannerType => bannerType;
    [SerializeField] BannerType bannerType;

    [Header("Characters")]
    [ReadOnly, SerializeField, ShowIf("bannerType", BannerType.RateUp)] List<Character> rateUpCharacters = new List<Character>();
    public List<Character> RateUpCharacters => rateUpCharacters;

    private void Awake()
    {
        GameManager.OnGameStart += RefreshBanner;
        GameManager.OnRoundEnd += RefreshBanner;

        gameObject.AddListenerToButton(() => SetAsCurrentBanner());
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= RefreshBanner;
        GameManager.OnRoundEnd -= RefreshBanner;
    }

    [Button]
    void RefreshBanner()
    {
        if (bannerType == BannerType.Regular)
            return;

        rateUpCharacters = new List<Character>();

        for (int i = 0; i < CharacterManager.Rarities.Count; i++)
        {
            List<Character> charactersOfSameRarity = CharacterManager.Rarities[i].FilterOnly(CharacterManager.Characters);
            Character character = charactersOfSameRarity.ChooseRandomElementInList();
            rateUpCharacters.Add(character);
        }
    }

    void SetAsCurrentBanner()
    {
        transform.GetComponentInParent<BannerManager>().InitialiseBanner(this);
    }
}
