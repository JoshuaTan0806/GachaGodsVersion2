using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
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
