using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Hat")]
public class HatData : ScriptableObject
{
    public GameObject HatPrefab => hatPrefab;
    [SerializeField] GameObject hatPrefab;
}
