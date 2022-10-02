using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject NodePrefab;
    Vector3 nodeSize => Vector3.one * 0.3f;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector3 spawnPos = new Vector3();
                spawnPos.x = transform.position.x - 1 + i;
                spawnPos.y = transform.position.y - 1 + j;

                GameObject g = Instantiate(NodePrefab, spawnPos, Quaternion.identity, transform);
                g.transform.localScale = nodeSize;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector3 spawnPos = new Vector3();
                spawnPos.x = transform.position.x - 1 + i;
                spawnPos.y = transform.position.y - 1 + j;

                Gizmos.DrawCube(spawnPos, nodeSize);
            }
        }
    }
}
