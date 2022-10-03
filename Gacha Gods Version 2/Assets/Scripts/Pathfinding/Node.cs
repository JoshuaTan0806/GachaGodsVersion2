using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Node : MonoBehaviour
{
    public bool IsAvailable => isAvailable;
    [ReadOnly, ShowInInspector] bool isAvailable => BattleManager.availableNodes.Contains(transform);
    public List<Transform> Neighbours => neighbours;
    [ReadOnly, ShowInInspector] List<Transform> neighbours = new();

    private void Awake()
    {
        BattleManager.availableNodes.Add(transform);
    }

    public void AddNeighbour(Node node)
    {
        if(!neighbours.Contains(node.transform))
        {
            neighbours.Add(node.transform);
        }
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
