using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/HairStyle")]
public class HairstyleData : ScriptableObject
{
    public GameObject HairstylePrefab => hairstylePrefab;
    [SerializeField] GameObject hairstylePrefab;
}
