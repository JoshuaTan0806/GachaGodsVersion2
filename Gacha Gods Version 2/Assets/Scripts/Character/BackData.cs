using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackData : ScriptableObject
{
    public GameObject BackPrefab => backPrefab;
    [SerializeField] GameObject backPrefab;
}
