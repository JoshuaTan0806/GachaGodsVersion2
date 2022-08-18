using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class BannerSlider : MonoBehaviour
{
    [SerializeField] AudioClip clickSFX;
    [SerializeField] float _snapThreshold;
    [SerializeField] float _speed;
    ScrollRect ScrollRect;
    RectTransform RectTransform;
    Vector2 destination = new Vector2();

    private void Awake()
    {
        ScrollRect = GetComponentInParent<ScrollRect>();
        RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BannerManager.CurrentBanner != BannerManager.Banners[ClosestBanner()])
        {
            BannerManager.CurrentBanner = BannerManager.Banners[ClosestBanner()];
            SoundManager.PlaySFX(clickSFX);
        }

        if (Input.GetMouseButtonDown(0))
            destination = Vector2.zero;

        if(destination == Vector2.zero)
        {
            Clamp();

            if (ShouldSnap())
            {
                MoveToBanner(ClosestBanner());
            }

            if (ScrollRect.velocity.magnitude == 0)
                MoveToBanner(ClosestBanner());
        }
        else
        {
            MoveToDestination();

            if (RectTransform.anchoredPosition == destination)
                destination = Vector2.zero;
        }
    }

    void Clamp()
    {
        if (RectTransform.anchoredPosition.y < -BannerManager.Banners.Keys.First().anchoredPosition.y)
        {
            SnapToBanner(ClosestBanner());
            ScrollRect.velocity = Vector2.zero;
        }

        if (RectTransform.anchoredPosition.y > -BannerManager.Banners.Keys.Last().anchoredPosition.y)
        {
            SnapToBanner(ClosestBanner());
            ScrollRect.velocity = Vector2.zero;
        }
    }

    public void MoveToBanner(RectTransform banner)
    {
        if (Input.GetMouseButton(0))
            return;

        Vector3 pos = RectTransform.anchoredPosition;
        pos.y = -banner.anchoredPosition.y;
        RectTransform.anchoredPosition = Vector3.MoveTowards(RectTransform.anchoredPosition, pos, _speed * Time.deltaTime);
    }

    void MoveToDestination()
    {
        RectTransform.anchoredPosition = Vector3.MoveTowards(RectTransform.anchoredPosition, destination, 10 * _speed * Time.deltaTime);
    }

    public void SetDestination(RectTransform banner)
    {
        destination = new Vector2(RectTransform.anchoredPosition.x, -banner.anchoredPosition.y);
    }

    bool ShouldSnap()
    {
        return ScrollRect.velocity.magnitude > 0 && ScrollRect.velocity.magnitude < _snapThreshold;
    }

    public void SnapToBanner(RectTransform banner)
    {
        Vector3 pos = RectTransform.anchoredPosition;
        pos.y = -banner.anchoredPosition.y;
        RectTransform.anchoredPosition = pos;
    }

    RectTransform ClosestBanner()
    {
        RectTransform Closest = BannerManager.Banners.Keys.First();

        float closestDist = Mathf.Infinity;

        foreach (var item in BannerManager.Banners)
        {
            float dist = Mathf.Abs(item.Key.anchoredPosition.y + RectTransform.anchoredPosition.y);

            if (dist < closestDist)
            {
                closestDist = dist;
                Closest = item.Key;
            }
        }

        return Closest;
    }
}
