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

    public Dictionary<Node, float> ActiveNeighbours => activeNeighbours;
    [ReadOnly, ShowInInspector] Dictionary<Node, float> activeNeighbours = new();

    public System.Action<Node> OnBecameInactive;
    public System.Action<Node> OnBecameActive;

    private void Awake()
    {
        BattleManager.availableNodes.Add(this);
    }

    public void AddNeighbour(Node node, float distance)
    {
        if (!neighbours.ContainsKey(node))
            neighbours.Add(node, distance);

        if (!activeNeighbours.ContainsKey(node))
            activeNeighbours.Add(node, distance);

        node.OnBecameActive -= ActivateNeighbour;
        node.OnBecameActive += ActivateNeighbour;
        node.OnBecameInactive -= DeactivateNeighbour;
        node.OnBecameInactive += DeactivateNeighbour;
    }

    public void ActivateNeighbour(Node node)
    {
        if (!activeNeighbours.ContainsKey(node))
            activeNeighbours.Add(node, neighbours[node]);
    }

    public void DeactivateNeighbour(Node node)
    {
        if (activeNeighbours.ContainsKey(node))
            activeNeighbours.Remove(node);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pathfinder pathfinder = collision.GetComponent<Pathfinder>();

        if (pathfinder == null)
            return;

        if (BattleManager.availableNodes.Contains(this))
            BattleManager.availableNodes.Remove(this);

        pathfinder.start = this;
        OnBecameInactive?.Invoke(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<AI>() == null)
            return;

        if (!BattleManager.availableNodes.Contains(this))
            BattleManager.availableNodes.Add(this);

        OnBecameActive?.Invoke(this);
    }
}
