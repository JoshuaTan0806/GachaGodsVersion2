using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeData : ScriptableObject
{
    public GameObject LShoePrefab => lshoePrefab;
    [SerializeField] GameObject lshoePrefab;
    public GameObject rShoePrefab => rshoePrefab;
    [SerializeField] GameObject rshoePrefab;
}
