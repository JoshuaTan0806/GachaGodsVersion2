using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "Character/StatDatas")]
public class StatDatas : ScriptableObject
{
    public List<StatData> Stats => stats;
    [SerializeField, ReadOnly] List<StatData> stats;

    [Button]
    void AddStat()
    {
        StatData stat = ScriptableObject.CreateInstance<StatData>();
        stat.name = name;
        stats.Add(stat);
        AssetDatabase.AddObjectToAsset(stat, this);
    }

    [Button]
    void RefreshNames()
    {
        foreach (var item in stats)
        {
            if (item == null)
                continue;

            item.name = name + " " + item.Stat.ToString();
        }
    }

    private void OnDestroy()
    {
        foreach (var item in stats)
        {
            if (item == null)
                continue;

            DestroyImmediate(item, true);
        }

        stats.Clear();
    }
}
