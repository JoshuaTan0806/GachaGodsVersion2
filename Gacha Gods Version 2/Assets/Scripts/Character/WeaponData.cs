using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Weapon")]
public class WeaponData : CosmeticData
{
    public const string ID = "Weapon";
    public GameObject LHWeaponPrefab => lhWeaponPrefab;
    [SerializeField] GameObject lhWeaponPrefab;
    public GameObject RHWeaponPrefab => rhWeaponPrefab;
    [SerializeField] GameObject rhWeaponPrefab;
}
