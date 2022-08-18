using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Attack")]
public class Attack : ScriptableObject
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;

    public string Description => description;
    [SerializeField] string description;
}
