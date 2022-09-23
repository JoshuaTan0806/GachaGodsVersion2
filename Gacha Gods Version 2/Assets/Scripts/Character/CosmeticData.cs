using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticData : ScriptableObject
{
    public bool IsUnlocked => isUnlocked;
    [SerializeField] protected bool isUnlocked = false;
    public Rarity Rarity => rarity;
    [SerializeField] protected Rarity rarity;
}
