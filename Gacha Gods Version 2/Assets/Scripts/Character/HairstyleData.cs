using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/HairStyle")]
public class HairstyleData : CosmeticData
{
    public const string ID = "Hairstyle";
    public GameObject HairstylePrefab => hairstylePrefab;
    [SerializeField] GameObject hairstylePrefab;
}
