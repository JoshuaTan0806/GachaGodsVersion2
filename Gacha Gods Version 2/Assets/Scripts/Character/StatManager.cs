using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/StatManager")]
public class StatManager : Factories.FactoryBase
{
    public static StatDictionary StatDictionary => statDictionary;
    static StatDictionary statDictionary = new StatDictionary();

    [SerializeField]
    List<StatData> stats;

    public override void Initialise()
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (!StatDictionary.ContainsKey(stats[i].Stat))
                statDictionary.Add(stats[i].Stat, stats[i]);
        }
    }

    public static StatData NullStat(Stat stat)
    {
        return Instantiate(statDictionary[stat]);
    }

    public static StatData CreateStat(Stat stat, float flat = 1, float percent = 0, float multiplier = 1)
    {
        StatData newStat = NullStat(stat);

        newStat.Flat = flat;
        newStat.Percent = percent;
        newStat.Multiplier = multiplier;
   
        return newStat;
    }
}
