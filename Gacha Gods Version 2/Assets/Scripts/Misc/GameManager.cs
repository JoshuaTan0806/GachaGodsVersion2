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

    public static int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            OnLevelChanged?.Invoke();
        }
    }
    static int level;
    public static System.Action OnLevelChanged;

    public static int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            OnGoldChanged?.Invoke();
        }
    }
    static int gold;
    public static System.Action OnGoldChanged;

    public static int Gems
    {
        get
        {
            return gems;
        }
        set
        {
            gems = value;
            OnGemsChanged?.Invoke();
        }
    }
    static int gems;
    public static System.Action OnGemsChanged;

    public static int Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;
            OnExperienceChanged?.Invoke();
        }
    }
    static int experience;
    public static System.Action OnExperienceChanged;

    private void Start()
    {
        //LoadGame();
        StartGame();
        RoundNumber = 1;
        Level = 5;
        Gold = 10;
        Gems = 0;
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

    public static void AddGold(int num)
    {
        Gold += num;
    }

    public static void RemoveGold(int num)
    {
        Gold -= num;
    }

    public static void AddGems(int num)
    {
        Gems += num;
    }

    public static void RemoveGems(int num)
    {
        Gems -= num;
    }

    public static void AddExperience(int num)
    {
        Experience += num;
    }

    public static void ResetExperience()
    {
        Experience = 0;
    }

    public static void AddLevel()
    {
        Level++;
    }

    void SpeedUp()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Time.timeScale = 5;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            Time.timeScale = 1;
    }
}
