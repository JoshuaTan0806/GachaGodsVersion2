using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Spell")]
public class Spell : ScriptableObject
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;

    public string Description => description;
    [SerializeField] string description;
}
