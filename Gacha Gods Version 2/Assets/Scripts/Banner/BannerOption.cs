using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerOption : MonoBehaviour
{
    RectTransform RectTransform;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        gameObject.AddListenerToButton(MoveToBanner);

        GameManager.OnGameStart += RefreshName;
        GameManager.OnRoundEnd += RefreshName;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= RefreshName;
        GameManager.OnRoundEnd -= RefreshName;
    }

    void RefreshName()
    {
        gameObject.name = BannerManager.Banners[RectTransform].name + " Option";
    }

    void MoveToBanner()
    {
        GetComponentInParent<BannerSlider>().SetDestination(RectTransform);
    }
}
