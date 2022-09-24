using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackData : ScriptableObject
{
    public const string ID = "Back";
    public GameObject BackPrefab => backPrefab;
    [SerializeField] GameObject backPrefab;
}
