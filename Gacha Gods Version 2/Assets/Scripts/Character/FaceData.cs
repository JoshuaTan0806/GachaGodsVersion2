using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Face")]
public class FaceData : CosmeticData
{
    public const string ID = "Face";
    public GameObject FacePrefab => facePrefab;
    [SerializeField] GameObject facePrefab;
}
