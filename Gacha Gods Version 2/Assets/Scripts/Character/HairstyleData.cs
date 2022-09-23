using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairstyleData : ScriptableObject
{
    public GameObject HairstylePrefab => hairstylePrefab;
    [SerializeField] GameObject hairstylePrefab;
}
