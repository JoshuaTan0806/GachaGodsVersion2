using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Node : MonoBehaviour
{
    public Vector3 position => transform.position;
    public bool IsAvailable => isAvailable;
    [ReadOnly, ShowInInspector] bool isAvailable => BattleManager.availableNodes.Contains(transform);
    public Dictionary<Transform, float> Neighbours => neighbours;
    [ReadOnly, ShowInInspector] Dictionary<Transform, float> neighbours = new();

    private void Awake()
    {
        BattleManager.availableNodes.Add(transform);
    }

    public void AddNeighbour(Node node, float distance)
    {
        if(!neighbours.ContainsKey(node.transform))
        {
            neighbours.Add(node.transform, distance);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pathfinder pathfinder = collision.GetComponent<Pathfinder>();

        if (pathfinder == null)
            return;

        if (BattleManager.availableNodes.Contains(transform))
            BattleManager.availableNodes.Remove(transform);

        pathfinder.currentNode = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<AI>() == null)
            return;

        if (!BattleManager.availableNodes.Contains(transform))
            BattleManager.availableNodes.Add(transform);
    }
}
