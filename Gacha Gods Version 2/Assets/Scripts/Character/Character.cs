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
    public Ability Attack => attack;
    [SerializeField, ReadOnly] Ability attack;
    public Ability Spell => spell;
    [SerializeField, ReadOnly] Ability spell;

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

        Ability attack = ScriptableObject.CreateInstance<Ability>();
        attack.name = "Attack";
        this.attack = attack;
        AssetDatabase.AddObjectToAsset(attack, this);

        Ability spell = ScriptableObject.CreateInstance<Ability>();
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