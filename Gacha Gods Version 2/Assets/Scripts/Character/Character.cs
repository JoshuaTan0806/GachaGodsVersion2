using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;
    public Sprite Icon => icon;
    [SerializeField] Sprite icon;
    public List<Trait> Traits => traits;
    [SerializeField] List<Trait> traits;
    public Rarity Rarity => rarity;
    [SerializeField] Rarity rarity;
    public StatFloatDictionary BaseStats => baseStats;
    [SerializeField] StatFloatDictionary baseStats;
    public List<Mastery> Masteries => masteries;
    [SerializeField, ReadOnly] List<Mastery> masteries;
    public Attack Attack => attack;
    [SerializeField, ReadOnly] Attack attack;
    public Spell Spell => spell;
    [SerializeField, ReadOnly] Spell spell;

    [Button]
    void CreateScriptables()
    {
        if (Attack != null)
            return;

        for (int i = 0; i < 6; i++)
        {
            Mastery mastery = ScriptableObject.CreateInstance<Mastery>();
            mastery.name = "Mastery " + (i + 1);
            masteries.Add(mastery);

            AssetDatabase.AddObjectToAsset(mastery, this);
        }

        Attack attack = ScriptableObject.CreateInstance<Attack>();
        attack.name = "Attack";
        this.attack = attack;
        AssetDatabase.AddObjectToAsset(attack, this);

        Spell spell = ScriptableObject.CreateInstance<Spell>();
        spell.name = "Spell";
        this.spell = spell;
        AssetDatabase.AddObjectToAsset(spell, this);

        EditorExtensionMethods.SaveAsset(this);
    }

    private void OnValidate()
    {
        attack.name = "Attack";
        spell.name = "Spell";

        for (int i = 0; i < masteries.Count; i++)
        {
            masteries[i].name = "M" + (i + 1);
        }
    }
}