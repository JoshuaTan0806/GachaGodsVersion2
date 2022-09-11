using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    CharacterStats stats;

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

    public void Initialise(CharacterStats stats)
    {
        this.stats = stats;
    }

    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
