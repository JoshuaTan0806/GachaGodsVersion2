using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Shoe")]
public class ShoeData : CosmeticData
{
    public const string ID = "Shoe";
    public GameObject LShoePrefab => lshoePrefab;
    [SerializeField] GameObject lshoePrefab;
    public GameObject RShoePrefab => rshoePrefab;
    [SerializeField] GameObject rshoePrefab;
}
