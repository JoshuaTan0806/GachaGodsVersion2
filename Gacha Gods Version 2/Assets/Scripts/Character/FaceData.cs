using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Appearance/Face")]
public class FaceData : ScriptableObject
{
    public GameObject FacePrefab => facePrefab;
    [SerializeField] GameObject facePrefab;
}
