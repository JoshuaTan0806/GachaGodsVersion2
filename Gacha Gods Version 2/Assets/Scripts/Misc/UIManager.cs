using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("References")]
    [SerializeField] GameObject uiCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject teamCanvas;
    [SerializeField] GameObject gachaCanvas;
    [SerializeField] GameObject achievementsCanvas;
    [SerializeField] GameObject characterCanvas;

    [Header("Prefabs")]
    [SerializeField] GameObject roundStartPrefab;
    [SerializeField] GameObject battleWonPrefab;
    [SerializeField] GameObject battleLostPrefab;
    [SerializeField] GameObject gameWonPrefab;
    [SerializeField] GameObject gameLostPrefab;

    public GameObject TransitionPrefab => transitionPrefab; 
    [SerializeField] GameObject transitionPrefab; 

    public CharacterPreview CharacterPreviewPrefab => characterPreviewPrefab;
    [SerializeField] CharacterPreview characterPreviewPrefab;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnBattleStart += OnBattleStart;
        GameManager.OnBattleWon += OnBattleWon;
        GameManager.OnBattleLost += OnBattleLost;
        GameManager.OnGameWon += OnGameWon;
        GameManager.OnGameLost += OnGameLost;

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
        GameManager.OnBattleWon -= OnBattleWon;
        GameManager.OnBattleLost -= OnBattleLost;
        GameManager.OnGameWon -= OnGameWon;
        GameManager.OnGameLost -= OnGameLost;
    }


    private void OnGameWon()
    {
        Instantiate(gameWonPrefab);
    }

    private void OnGameLost()
    {
        Instantiate(gameLostPrefab);
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

    void OnBattleWon()
    {
        Instantiate(battleWonPrefab);
    }

    void OnBattleLost()
    {
        Instantiate(battleLostPrefab);
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
