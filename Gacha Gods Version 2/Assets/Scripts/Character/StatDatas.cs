using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "Character/StatDatas")]
public class StatDatas : ScriptableObject
{
    public List<StatData> Stats => stats;
    [SerializeField] List<StatData> stats;

    [Button]
    void AddStat()
    {
        StatData stat = ScriptableObject.CreateInstance<StatData>();
        stat.name = "Stat";
        stats.Add(stat);
        AssetDatabase.AddObjectToAsset(stat, this);
    }
}
