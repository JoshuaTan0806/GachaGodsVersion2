using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderByYPosition : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SpriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100) + 10000;
    }
}
