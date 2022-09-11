using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    CharacterStats stats;
    AbilityData ability;

    private void OnEnable()
    {
        GameManager.OnBattleWon += DestroyGameobject;
        GameManager.OnBattleLost += DestroyGameobject;
    }

    private void OnDisable()
    {
        GameManager.OnBattleWon -= DestroyGameobject;
        GameManager.OnBattleLost -= DestroyGameobject;
    }

    public void Initialise(CharacterStats stats, AbilityData ability)
    {
        this.stats = stats;
        this.ability = ability;
    }

    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
