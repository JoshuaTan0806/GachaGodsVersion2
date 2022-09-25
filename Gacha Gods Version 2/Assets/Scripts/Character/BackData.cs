using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Back")]
public class BackData : ScriptableObject
{
    public const string ID = "Back";
    public GameObject BackPrefab => backPrefab;
    [SerializeField] GameObject backPrefab;
}
