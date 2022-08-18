using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Role")]
public class Role : ScriptableObject
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
        return characters.Where(x => x.Roles.Contains(this)).ToList();
    }

    public List<Character> FilterOut(List<Character> characters)
    {
        return characters.Where(x => !x.Roles.Contains(this)).ToList();
    }
}
