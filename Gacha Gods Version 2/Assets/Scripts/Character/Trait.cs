using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "Character/Trait")]
public class Trait : ScriptableObject
{
    public string Name => name;
    public string Description => description;
    [SerializeField] string description;
    public Sprite Icon => icon;
    [SerializeField] Sprite icon;

    [SerializeField] bool needExact;
    public SetData SetData => setData;
    [SerializeField] SetData setData;

    public List<Character> FilterOnly(List<Character> characters)
    {
        return characters.Where(x => x.Traits.Contains(this)).ToList();
    }

    public List<Character> FilterOut(List<Character> characters)
    {
        return characters.Where(x => !x.Traits.Contains(this)).ToList();
    }

    public int FindLowestNumberForSet(int num)
    {
        if (SetData.IsNullOrEmpty())
            return 0;

        if(needExact)
        {
            if (!SetData.ContainsKey(num))
                return 0;
        }

        //need to find the highest number that exists in the setdata that is lower than num
        int setNum = num;

        while (!SetData.ContainsKey(setNum))
        {
            if (setNum == 0)
                return 0;

            setNum--;
        }

        return setNum;
    }

    public StatDatas FindStats(int num)
    {
        if (SetData.IsNullOrEmpty())
            return null;

        int setNum = FindLowestNumberForSet(num);

        if (setNum == 0)
            return null;

        return SetData[setNum];
    }

    [Button]
    void AddSetData()
    {
        for (int i = setData.Count; i < 5; i++)
        {
            StatDatas stats = ScriptableObject.CreateInstance<StatDatas>();
            stats.name = "S" + i.ToString();
            setData.Add(i, stats);
            AssetDatabase.AddObjectToAsset(stats, this);
        }
    }

    private void OnValidate()
    {
        foreach (var item in SetData)
        {
            item.Value.name = "S" + item.Key.ToString();
        }
    }
}
