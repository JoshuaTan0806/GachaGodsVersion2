using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitData : ScriptableObject
{
    public GameObject TopPrefab => topPrefab;
    [SerializeField] GameObject topPrefab;
    public GameObject LSleevePrefab => lSleevePrefab;
    [SerializeField] GameObject lSleevePrefab;
    public GameObject RSleevePrefab => rSleevePrefab;
    [SerializeField] GameObject rSleevePrefab;
    public GameObject BottomPrefab => bottomPrefab;
    [SerializeField] GameObject bottomPrefab;
}
