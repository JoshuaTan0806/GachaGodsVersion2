using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Character/Rarity")]
public class Rarity : ScriptableObject
{
    public string Name => _name;
    [SerializeField] string _name;
    public int RarityNumber => rarityNumber;
    [SerializeField] int rarityNumber;
    public Gradient Gradient => gradient;
    [SerializeField] Gradient gradient;
    public Sprite Icon => icon;
    [SerializeField] Sprite icon;

    public List<Character> FilterOnly(List<Character> characters)
    {
        return characters.Where(x => x.Rarity == this).ToList();
    }

    public List<Character> FilterOut(List<Character> characters)
    {
        return characters.Where(x => x.Rarity != this).ToList();
    }
}
