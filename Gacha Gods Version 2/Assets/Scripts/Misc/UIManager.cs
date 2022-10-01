using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("References")]
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject teamCanvas;
    [SerializeField] GameObject gachaCanvas;
    [SerializeField] GameObject achievementsCanvas;
    [SerializeField] GameObject characterCanvas;
    [SerializeField] GameObject battleCanvas;

    [Header("Prefabs")]
    [SerializeField] GameObject roundStartPrefab;
    [SerializeField] GameObject battleWonPrefab;
    [SerializeField] GameObject battleLostPrefab;
    [SerializeField] GameObject gameWonPrefab;
    [SerializeField] GameObject gameLostPrefab;
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
        mainCanvas.SafeSetActive(true);
        battleCanvas.SafeSetActive(false);
    }

    void OnBattleStart()
    {
        mainCanvas.SafeSetActive(false);
        battleCanvas.SafeSetActive(true);
    }

    void OnBattleWon()
    {
        Instantiate(battleWonPrefab);
    }

    void OnBattleLost()
    {
        Instantiate(battleLostPrefab);
    }

    void SpawnRoundStartPrefab()
    {
        Instantiate(roundStartPrefab);
    }

    public void SpawnTransition(UnityEvent unityEvent)
    {
        Transition t = Instantiate(transitionPrefab).GetComponent<Transition>();
        t.Initialise(unityEvent);
    }

    public void SpawnTransition(Action action)
    {
        Transition t = Instantiate(transitionPrefab).GetComponent<Transition>();
        t.Initialise(action);
    }
}
