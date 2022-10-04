using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Node : MonoBehaviour
{
    public Vector3 position => transform.position;
    public bool IsActive => isActive;
    [ReadOnly, ShowInInspector] bool isActive => BattleManager.availableNodes.Contains(this);
    public Dictionary<Node, float> Neighbours => neighbours;
    [ReadOnly, ShowInInspector] Dictionary<Node, float> neighbours = new();

    public Dictionary<Node, float> ActiveNeighbours => activeNeighbours;
    [ReadOnly, ShowInInspector] Dictionary<Node, float> activeNeighbours = new();

    public System.Action<Node> OnBecameInactive;
    public System.Action<Node> OnBecameActive;

    [ReadOnly, ShowInInspector] List<Pathfinder> characters = new();

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

        if (!characters.Contains(pathfinder))
            characters.Add(pathfinder);

        pathfinder.start = this;

        //deactivate the node

        if (BattleManager.availableNodes.Contains(this))
            BattleManager.availableNodes.Remove(this);

        OnBecameInactive?.Invoke(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Pathfinder pathfinder = collision.GetComponent<Pathfinder>();

        if (pathfinder == null)
            return;

        if (characters.Contains(pathfinder))
            characters.Remove(pathfinder);

        //only reactivate if there are no more characters on this spot

        if (characters.Count > 0)
            return;

        if (!BattleManager.availableNodes.Contains(this))
            BattleManager.availableNodes.Add(this);

        OnBecameActive?.Invoke(this);
    }
}
