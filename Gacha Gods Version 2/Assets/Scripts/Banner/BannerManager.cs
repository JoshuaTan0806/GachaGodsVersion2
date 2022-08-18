using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BannerManager : MonoBehaviour
{
    public static Dictionary<RectTransform, Banner> Banners = new Dictionary<RectTransform, Banner>();

    public static Banner CurrentBanner
    {
        get
        {
            return currentBanner;
        }
        set
        {
            currentBanner = value;
            ChangeBanner();
        }
    }
    static Banner currentBanner;
   
    [Header("References")]
    [SerializeField] Transform GachaHolder;
    [SerializeField] Transform BannerHolder;
    [SerializeField] Transform BannerOptionHolder;

    [Header("Prefabs")]
    [SerializeField] GameObject BannerPrefab;
    [SerializeField] GameObject BannerOptionPrefab;

    [Header("Names")]
    [SerializeField] List<string> bannerNames;
    public static List<string> BannerNames;

    private void Awake()
    {
        GameManager.OnRoundEnd += ResetBannerNames;
        GameManager.OnRoundEnd += SnapToFirstBanner;
        ResetBannerNames();
        InitialiseBanners();
        currentBanner = Banners.Values.First();
        ChangeBanner();
    }

    private void OnDestroy()
    {
        GameManager.OnRoundEnd -= ResetBannerNames;
        GameManager.OnRoundEnd -= SnapToFirstBanner;
    }

    void ResetBannerNames()
    {
        BannerNames = new List<string>(bannerNames);
    }

    void InitialiseBanners()
    {
        Banners = new Dictionary<RectTransform, Banner>();

        SpawnBanner(BannerType.Regular);

        for (int i = 0; i < 3; i++)
        {
            SpawnBanner(BannerType.RateUp);
        }

        SnapToFirstBanner();
    }

    void SpawnBanner(BannerType bannerType)
    {
        Banner b = Instantiate(BannerPrefab, BannerHolder).GetComponent<Banner>();
        b.bannerType = bannerType;
        RectTransform t = Instantiate(BannerOptionPrefab, BannerOptionHolder).GetComponent<RectTransform>();
        Banners.Add(t, b);
    }

    static void ChangeBanner()
    {
        foreach (var item in Banners)
        {
            item.Value.gameObject.SafeSetActive(false);
        }

        CurrentBanner.gameObject.SafeSetActive(true);
    }

    void SnapToFirstBanner()
    {
        GetComponentInChildren<BannerSlider>().SnapToBanner(Banners.First().Key);
    }
}
