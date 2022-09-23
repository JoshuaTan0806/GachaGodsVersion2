using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceData : ScriptableObject
{
    public GameObject FacePrefab => facePrefab;
    [SerializeField] GameObject facePrefab;
}
