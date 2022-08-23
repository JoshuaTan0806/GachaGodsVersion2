using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopItems
{
    XP,
    Star,
    Gem
}

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class BuyButton : MonoBehaviour
{
    [SerializeField] ShopItems shopItem;
    [SerializeField] int cost;
    [SerializeField] int amount;

    private void Awake()
    {
        gameObject.AddListenerToButton(() => Buy());
    }

    void Buy()
    {
        if (GameManager.Gold < cost)
            return;

        GameManager.RemoveGold(cost);

        switch (shopItem)
        {
            case ShopItems.XP:
                GameManager.AddExperience(amount);
                break;
            case ShopItems.Star:
                GameManager.AddStars(amount);
                break;
            case ShopItems.Gem:
                GameManager.AddGems(amount);
                break;
            default:
                break;
        }
    }
}
