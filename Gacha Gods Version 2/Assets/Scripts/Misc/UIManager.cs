using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject uiCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject teamCanvas;
    [SerializeField] GameObject gachaCanvas;
    [SerializeField] GameObject achievementsCanvas;
    [SerializeField] GameObject characterCanvas;

    [Header("Prefabs")]
    [SerializeField] GameObject roundStartPrefab;

    private void OnEnable()
    {
        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnBattleStart += OnBattleStart;

        uiCanvas.SafeSetActive(true);
        mainCanvas.SafeSetActive(true);
        teamCanvas.SafeSetActive(false);
        gachaCanvas.SafeSetActive(false);
        achievementsCanvas.SafeSetActive(false);
        characterCanvas.SafeSetActive(false);
    }

    private void OnDisable()
    {
        GameManager.OnRoundStart -= OnRoundStart;
        GameManager.OnBattleStart -= OnBattleStart;
    }
    
    void OnRoundStart()
    {
        SpawnRoundStartPrefab();
        ActivateMainCanvas();
    }

    void OnBattleStart()
    {
        DeactivateMainCanvas();
    }

    void ActivateMainCanvas()
    {
        uiCanvas.SafeSetActive(true);
        mainCanvas.SafeSetActive(true);
    }

    void DeactivateMainCanvas()
    {
        uiCanvas.SafeSetActive(false);
    }

    void SpawnRoundStartPrefab()
    {
        Instantiate(roundStartPrefab);
    }
}