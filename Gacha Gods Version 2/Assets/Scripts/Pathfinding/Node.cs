using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Node : MonoBehaviour
{
    public Vector3 position => transform.position;
    public bool IsAvailable => isAvailable;
    [ReadOnly, ShowInInspector] bool isAvailable => BattleManager.availableNodes.Contains(this);
    public Dictionary<Node, float> Neighbours => neighbours;
    [ReadOnly, ShowInInspector] Dictionary<Node, float> neighbours = new();

    private void Awake()
    {
        BattleManager.availableNodes.Add(this);
    }

    public void AddNeighbour(Node node, float distance)
    {
        if(!neighbours.ContainsKey(node))
        {
            neighbours.Add(node, distance);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pathfinder pathfinder = collision.GetComponent<Pathfinder>();

        if (pathfinder == null)
            return;

        if (BattleManager.availableNodes.Contains(this))
            BattleManager.availableNodes.Remove(this);

        pathfinder.start = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<AI>() == null)
            return;

        if (!BattleManager.availableNodes.Contains(this))
            BattleManager.availableNodes.Add(this);
    }
}
