using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    public GameObject LHWeaponPrefab => lhWeaponPrefab;
    [SerializeField] GameObject lhWeaponPrefab;
    public GameObject RHWeaponPrefab => rhWeaponPrefab;
    [SerializeField] GameObject rhWeaponPrefab;
}
