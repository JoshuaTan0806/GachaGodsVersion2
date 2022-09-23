using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatData : ScriptableObject
{
    public GameObject HatPrefab => hatPrefab;
    [SerializeField] GameObject hatPrefab;
}
