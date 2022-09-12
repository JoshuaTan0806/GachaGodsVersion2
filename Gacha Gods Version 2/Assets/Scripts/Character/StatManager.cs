using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "Managers/StatManager")]
public class StatManager : Factories.FactoryBase
{
    public static StatDictionary StatDictionary => statDictionary;
    static StatDictionary statDictionary = new StatDictionary();
    [SerializeField, ReadOnly] StatDictionary stats;

    public override void Initialise()
    {
        statDictionary = stats;
    }

    public static StatData NullStat(Stat stat)
    {
        return Instantiate(statDictionary[stat]);
    }

    public static StatData CreateStat(Stat stat, float flat = 0, float percent = 0, float multiplier = 1)
    {
        StatData newStat = NullStat(stat);

        newStat.Flat = flat;
        newStat.Percent = percent;
        newStat.Multiplier = multiplier;
   
        return newStat;
    }

    [Button]
    void CreateStats()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(Stat)).Length; i++)
        {
            if (stats.ContainsKey((Stat)i))
                continue;

            StatData stat = ScriptableObject.CreateInstance<StatData>();
            stat.name = ((Stat)i).ToString();
            stat.SetStat((Stat)i);
            AssetDatabase.AddObjectToAsset(stat, this);
            stats.Add((Stat)i, stat);
        }
    }
}
