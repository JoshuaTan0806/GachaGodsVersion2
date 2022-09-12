using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    CharacterStats stats;
    AbilityData ability;

    private void OnEnable()
    {
        GameManager.OnBattleWon += DestroyGameObject;
        GameManager.OnBattleLost += DestroyGameObject;
    }

    private void OnDisable()
    {
        GameManager.OnBattleWon -= DestroyGameObject;
        GameManager.OnBattleLost -= DestroyGameObject;
    }

    public void Initialise(CharacterStats stats, AbilityData ability)
    {
        this.stats = stats;
        this.ability = ability;
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
