using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Trait")]
public class Trait : ScriptableObject
{
    public string Name => name;

    public string Description => description;
    [SerializeField] string description;
    public Sprite Icon => icon;
    [SerializeField] Sprite icon;
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
}
