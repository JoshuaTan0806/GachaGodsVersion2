using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static System.Action OnGameStart;
    public static System.Action OnGameWon;
    public static System.Action OnGameLost;
    public static System.Action OnGameEnd;
    public static System.Action OnRoundStart;
    public static System.Action OnBattleStart;
    public static System.Action OnRoundEnd;
    public static System.Action OnBattleWon;
    public static System.Action OnBattleLost;

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

    public static int Stars
    {
        get
        {
            return stars;
        }
        set
        {
            stars = value;
            OnStarsChanged?.Invoke();
        }
    }
    static int stars;
    public static System.Action OnStarsChanged;

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

    public static int Wins
    {
        get
        {
            return wins;
        }
        set
        {
            wins = value;
        }
    }
    static int wins;

    public static int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            OnHealthChanged?.Invoke();
        }
    }
    static int health;
    public static System.Action OnHealthChanged;

    private void Start()
    {
        //LoadGame();
        RoundNumber = 1;
        Health = 20;
        Experience = 0;
        Wins = 0;
        Level = 3;
        stars = 10;
        Gold = 100;
        Gems = 0;
        StartGame();
    }

    [Button]
    public static void StartGame()
    {
        OnGameStart?.Invoke();
        StartRound();
    }

    public static void WinGame()
    {
        OnGameWon?.Invoke();
        OnGameEnd?.Invoke();
    }

    public static void LoseGame()
    {
        OnGameLost?.Invoke();
        OnGameEnd?.Invoke();
    }

    [Button]
    public static void StartRound()
    {
        OnRoundStart?.Invoke();
    }

    [Button]
    public static void StartBattle()
    {
        OnBattleStart?.Invoke();
    }

    [Button]
    public static void EndRound()
    {
        OnRoundEnd?.Invoke();

        int interest = Mathf.Min(gold / 5, 5);
        AddGold(interest);
        AddGold(10);
        RoundNumber++;
        StartRound();
    }

    private void Update()
    {
#if UNITY_EDITOR
        SpeedUp();
#endif
    }

    public static void WinBattle()
    {
        OnBattleWon?.Invoke();
        Wins++;

        if (Wins == 10)
            WinGame();
    }

    public static void LoseBattle(int enemiesRemaining)
    {
        OnBattleLost?.Invoke();
        Health -= enemiesRemaining;

        if (health <= 0)
            LoseGame();
    }

    public static void AddStars(int num)
    {
        Stars += num;
    }

    public static void RemoveStars(int num)
    {
        Stars -= num;
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

        if(Experience >= RequiredXP())
        {
            Experience -= RequiredXP();
            AddLevel();
        }
    }

    public static void AddLevel()
    {
        if (Level == 10)
            return;

        Level++;
    }

    public static int RequiredXP()
    {
        return (level - 2) * 5;
    }

    void SpeedUp()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Time.timeScale = 3;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            Time.timeScale = 1;
    }
}
