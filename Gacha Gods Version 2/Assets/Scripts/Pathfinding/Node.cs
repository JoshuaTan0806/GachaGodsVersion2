using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Node : MonoBehaviour
{
    [ReadOnly, ShowInInspector] bool isAvailable => BattleManager.availableNodes.Contains(transform);

    private void Awake()
    {
        BattleManager.availableNodes.Add(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AI>() == null)
            return;

        if (BattleManager.availableNodes.Contains(transform))
            BattleManager.availableNodes.Remove(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<AI>() == null)
            return;

        if (!BattleManager.availableNodes.Contains(transform))
            BattleManager.availableNodes.Add(transform);
    }
}
