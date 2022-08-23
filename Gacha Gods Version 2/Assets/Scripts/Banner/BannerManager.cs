using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BannerManager : MonoBehaviour
{
    Banner currentBanner;
    [SerializeField] int RateUpChances = 10;

    [Header("References")]
    [SerializeField] List<Banner> banners;
    [SerializeField] Button PremiumRollButtonReference;
    [SerializeField] Button OneRollButtonReference;
    [SerializeField] Button TenRollButtonReference;
    [SerializeField] Transform RateUpReference;
    [SerializeField] List<ViewCharacterPreview> RateUpCharactersPositionReference;
    [SerializeField] List<Image> RarityOddsReference;

    [Header("Prefabs")]
    [SerializeField] GachaChoice GachaChoice;

    private void Awake()
    {
        currentBanner = banners[0];

        OneRollButtonReference.AddListenerToButton(Use1Star);
        PremiumRollButtonReference.AddListenerToButton(Use1Gem);
        TenRollButtonReference.AddListenerToButton(Use10Stars);
    }

    private void OnEnable()
    {
        InitialiseBanner(currentBanner);
        UpdateRarityOdds();
    }

    public void InitialiseBanner(Banner banner)
    {
        currentBanner = banner;

        if (banner.BannerType == BannerType.Regular)
        {
            RateUpReference.gameObject.SafeSetActive(false);
            TenRollButtonReference.gameObject.SetActive(true);
            OneRollButtonReference.gameObject.SetActive(true);
        }
        else
        {
            RateUpReference.gameObject.SafeSetActive(true);
            TenRollButtonReference.gameObject.SetActive(false);
            OneRollButtonReference.gameObject.SetActive(false);

            for (int i = 0; i < RateUpCharactersPositionReference.Count; i++)
            {
                RateUpCharactersPositionReference[i].Initialise(banner.RateUpCharacters[i]);
            }
        }
    }

    void Use1Star()
    {
        if (GameManager.Stars < 1)
            return;
        else
        {
            GameManager.RemoveStars(1);
            StartCoroutine(RollGacha(1));
        }
    }

    void Use10Stars()
    {
        if (GameManager.Stars < 10)
            return;
        else
        {
            GameManager.RemoveStars(10);
            StartCoroutine(RollGacha(10));
        }
    }

    void Use1Gem()
    {
        if (GameManager.Gems < 1)
            return;
        else
        {
            GameManager.RemoveGems(1);
            StartCoroutine(RollGacha(1));
        }
    }

    void UpdateRarityOdds()
    {
        OddsDictionary odds = FindOdds(GameManager.Level);

        for (int i = 0; i < CharacterManager.Rarities.Count; i++)
        {
            RarityOddsReference[i].color = CharacterManager.Rarities[i].Gradient.Evaluate(0);
            RarityOddsReference[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(odds[CharacterManager.Rarities[i]] + "%");
        }
    }

    IEnumerator RollGacha(int numberOfRolls)
    {
        int counter = 0;
        GachaChoice gachaPrefab = Instantiate(GachaChoice);

        while (counter < numberOfRolls)
        {
            Rarity rarityToRollAt;

            if (counter == 9)
                rarityToRollAt = RollRarity(GameManager.Level + 1);
            else
                rarityToRollAt = RollRarity(GameManager.Level);

            Dictionary<Character, bool> characters = new Dictionary<Character, bool>();

            for (int i = 0; i < 3; i++)
            {
                Character character = RollCharacterOfRarity(rarityToRollAt);

                while(characters.ContainsKey(character))
                    character = RollCharacterOfRarity(rarityToRollAt);

                characters.Add(character, currentBanner.RateUpCharacters.Contains(character));
            }

            gachaPrefab.Initialise(characters);

            while (!gachaPrefab.HasBeenPicked)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.1f);

            counter++;
        }

        Destroy(gachaPrefab.gameObject);
    }

    Rarity RollRarity(int level)
    {
        OddsDictionary odds = FindOdds(level);

        float roll = Random.Range(0, 100);
        float counter = 0;

        foreach (var item in odds)
        {
            counter += item.Value;

            if (roll < counter)
            {
                return item.Key;
            }
        }

        throw new System.Exception("Roll total is above 100.");
    }

    Character RollCharacterOfRarity(Rarity rarity)
    {
        //pull a character
        List<Character> charactersOfSameRarity = rarity.FilterOnly(CharacterManager.Characters);
        Character characterPulled = charactersOfSameRarity.ChooseRandomElementInList();

        //if theres rate up, we have a chance of overriding that with the rate up
        if (currentBanner.RateUpCharacters.Where(x => x.Rarity == rarity).ToList().Count > 0)
        {
            int num = Random.Range(0, 100);
            if (num < RateUpChances)
            {
                characterPulled = currentBanner.RateUpCharacters.Where(x => x.Rarity == rarity).ToList()[0];
            }
        }

        return characterPulled;
    }

    OddsDictionary FindOdds(int level)
    {
        return CharacterManager.Odds[level];
    }
}
