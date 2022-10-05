using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class Pathfinder : MonoBehaviour
{
    [ReadOnly] public Node start;
    [ReadOnly] public List<Node> path = new();
    Color32 color = new();

    private void Awake()
    {
        color = color.Randomise();
    }

    public Node FindTargetNode(Node goal)
    {
        Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
        Dictionary<Node, float> cost_so_far = new Dictionary<Node, float>();

        List<Node> path = new List<Node>();

        Queue<Node> frontier = new Queue<Node>();
        frontier.Enqueue(start);

        came_from.Add(start, start);
        cost_so_far.Add(start, 0);

        Node current = new();
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            if (current == goal) break; // Early exit

            //we want to add the goal to the list even if it has been deactivated
            Dictionary<Node, float> activeNeighbours = new Dictionary<Node, float>(current.ActiveNeighbours);
            if (current.Neighbours.ContainsKey(goal) && !activeNeighbours.ContainsKey(goal))
                activeNeighbours.Add(goal, current.Neighbours[goal]);

            foreach (var next in activeNeighbours.ToList())
            {
                float new_cost = cost_so_far[current] + next.Value;
            
                if (!cost_so_far.ContainsKey(next.Key) || new_cost < cost_so_far[next.Key])
                {
                    cost_so_far[next.Key] = new_cost;
                    came_from[next.Key] = current;
                    frontier.Enqueue(next.Key);
                }
            }
        }

        while (current != start)
        {
            path.Add(current);
            current = came_from[current];
        }

        //for gizmos
        this.path = new List<Node>(path);

        path.Remove(start);

        if (path.Count > 1)
            return path[^1];
        else
            return start;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.1f);

        for (int i = 0; i < path.Count; i++)
        {
            if (i > 0)
                Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
            else
                Gizmos.DrawLine(start.position, path[i].transform.position);
        }
    }
}
