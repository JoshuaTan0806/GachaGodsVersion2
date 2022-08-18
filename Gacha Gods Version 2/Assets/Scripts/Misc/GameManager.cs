using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static System.Action OnGameStart;
    public static System.Action OnGameEnd;
    public static System.Action OnRoundStart;
    public static System.Action OnRoundEnd;
    public static System.Action OnRoundWon;
    public static System.Action OnRoundLost;

    public static int RoundNumber;

    private void Start()
    {
        //LoadGame();
        StartGame();
        RoundNumber = 1;
    }

    [Button]
    public static void StartGame()
    {
        OnGameStart?.Invoke();
    }

    [Button]
    public static void EndGame()
    {
        OnGameEnd?.Invoke();
        RoundNumber++;
    }

    [Button]
    public static void StartRound()
    {
        OnRoundStart?.Invoke();
    }

    [Button]
    public static void EndRound()
    {
        OnRoundEnd?.Invoke();
    }

    private void Update()
    {
#if UNITY_EDITOR
        SpeedUp();
#endif
    }

    void SpeedUp()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Time.timeScale = 5;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            Time.timeScale = 1;
    }
}
