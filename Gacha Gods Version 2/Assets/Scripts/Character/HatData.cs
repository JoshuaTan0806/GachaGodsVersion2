using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Hat")]
public class HatData : CosmeticData
{
    public const string ID = "Hat";
    public GameObject HatPrefab => hatPrefab;
    [SerializeField] GameObject hatPrefab;
}
