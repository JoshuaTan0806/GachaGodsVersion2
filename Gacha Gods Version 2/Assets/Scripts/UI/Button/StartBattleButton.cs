using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButton : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddListenerToButton(() => StartBattle());
    }

    void StartBattle()
    {
        if (CharacterManager.ActiveCharacters.Count == 0)
            return;

        TransitionManager.SpawnTransition(() => GameManager.StartBattle());
    }
}
