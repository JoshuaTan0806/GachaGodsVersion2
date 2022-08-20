using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/StatDatas")]
public class StatDatas : ScriptableObject
{
    public List<StatData> Stats => stats;
    [SerializeField] List<StatData> stats;
}
