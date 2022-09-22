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
    public List<AbilityData> Attacks => attacks;
    [SerializeField, ReadOnly] List<AbilityData> attacks;
    public List<AbilityData> Spells => spells;
    [SerializeField, ReadOnly] List<AbilityData> spells;

    [SerializeField] AudioClip themeSong;

    [Button]
    void CreateMasteries()
    {
        for (int i = 0; i < 6; i++)
        {
            Mastery mastery = ScriptableObject.CreateInstance<Mastery>();
            mastery.name = "Mastery " + (i + 1);
            masteries.Add(mastery);

            AssetDatabase.AddObjectToAsset(mastery, this);
        }
        EditorExtensionMethods.SaveAsset(this);
    }

    [Button]
    void CreateAttack()
    {
        AbilityData attack = ScriptableObject.CreateInstance<AbilityData>();
        attack.name = "Attack " + (attacks.Count + 1);
        attacks.Add(attack);
        AssetDatabase.AddObjectToAsset(attack, this);
    }

    [Button]
    void CreateSpell()
    {
        AbilityData spell = ScriptableObject.CreateInstance<AbilityData>();
        spell.name = "Spell " + (spells.Count + 1);
        spells.Add(spell);
        AssetDatabase.AddObjectToAsset(spell, this);
    }

    private void OnValidate()
    {
        for (int i = attacks.Count - 1; i >= 0; i--)
        {
            if (attacks[i] == null)
                attacks.RemoveAt(i);

            attacks[i].name = "Attack " + (i + 1);
        }

        for (int i = spells.Count - 1; i >= 0; i--)
        {
            if (spells[i] == null)
                spells.RemoveAt(i);

            spells[i].name = "Spell " + (i + 1);
        }


        for (int i = 0; i < masteries.Count; i++)
        {
            masteries[i].name = "M" + (i + 1);
        }
    }

    public void PlayThemeSong()
    {
        if (themeSong != null)
            SoundManager.PlaySFX(themeSong);
    }
}