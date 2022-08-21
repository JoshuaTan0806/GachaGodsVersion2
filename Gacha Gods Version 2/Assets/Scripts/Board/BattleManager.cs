using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static List<CharacterStats> allies = new List<CharacterStats>();
    public static List<CharacterStats> enemies = new List<CharacterStats>();

    public static List<CharacterStats> targetableAllies = new List<CharacterStats>();
    public static List<CharacterStats> targetableEnemies = new List<CharacterStats>();

    public static BoardData playerBoardData;
    public static BoardData enemyBoardData;

    [Header("References")]
    [SerializeField] List<Transform> spawnPoints;

    [Header("Prefabs")]
    [SerializeField] BattleStartCanvas battleStartPrefab;
    [SerializeField] CharacterStats BaseCharacterPrefab;

    private void Awake()
    {
        ClearBoard();
    }

    private void OnEnable()
    {
        GameManager.OnBattleStart += OnBattleStart;
        GameManager.OnRoundEnd += ClearBoard;
    }

    private void OnDisable()
    {
        GameManager.OnBattleStart -= OnBattleStart;
        GameManager.OnRoundEnd -= ClearBoard;
    }
 
    public static List<CharacterStats> FindEnemies(CharacterStats stats)
    {
        if (allies.Contains(stats))
            return targetableEnemies;
        else
            return targetableAllies;
    }

    public void LoadBoardData()
    {
        BoardData boardData = Board.PlayerBoard();

        playerBoardData = boardData;

        List<StatData> globalBuffs = new List<StatData>();

        foreach (var item in boardData.Archetypes)
        {
            StatDatas stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats.Stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.Roles)
        {
            StatDatas stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats.Stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = spawnPoints[item.Position].position;
            CharacterStats stats = Instantiate(BaseCharacterPrefab, spawnPos, Quaternion.identity, transform);
            allies.Add(stats);
            stats.InitialiseCharacter(item.Character);
            Instantiate(item.Character.Prefab, stats.transform);

            for (int i = 0; i < item.Mastery; i++)
            {
                Mastery mastery = item.Character.Mastery[i];

                switch (mastery.MasteryType)
                {
                    case MasteryType.Stat:
                        foreach (var stat in mastery.Stats)
                        {
                            stats.AddStat(stat);
                        }
                        break;
                    case MasteryType.GlobalStat:
                        foreach (var stat in mastery.GlobalStats)
                        {
                            globalBuffs.Add(stat);
                        }
                        break;
                    case MasteryType.Attack:
                        stats.UpgradeAttack(mastery.Attack);
                        break;
                    case MasteryType.Spell:
                        stats.UpgradeSpell(mastery.Spell);
                        break;
                    default:
                        break;
                }
            }
        }

        foreach (var ally in allies)
        {
            foreach (var stat in globalBuffs)
            {
                ally.AddStat(stat);
            }
        }
    }
    public void LoadEnemyBoardData()
    {
        if (BoardDatabase.DatabaseIsEmpty(GameManager.RoundNumber))
        {
            Board.SaveBoardData();
        }

        BoardData boardData = BoardDatabase.LoadEnemyBoard(GameManager.RoundNumber);

        enemyBoardData = boardData;

        List<StatData> globalBuffs = new List<StatData>();

        foreach (var item in boardData.Archetypes)
        {
            StatDatas stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats.Stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.Roles)
        {
            StatDatas stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats.Stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = spawnPoints[spawnPoints.Count - 1 - item.Position].position;
            CharacterStats stats = Instantiate(BaseCharacterPrefab, spawnPos, Quaternion.identity, transform);
            stats.InitialiseCharacter(item.Character);
            Instantiate(item.Character.Prefab, stats.transform);

            for (int i = 0; i < item.Mastery; i++)
            {
                Mastery mastery = item.Character.Mastery[i];

                switch (mastery.MasteryType)
                {
                    case MasteryType.Stat:
                        foreach (var stat in mastery.Stats)
                        {
                            stats.AddStat(stat);
                        }
                        break;
                    case MasteryType.GlobalStat:
                        foreach (var stat in mastery.GlobalStats)
                        {
                            globalBuffs.Add(stat);
                        }
                        break;
                    case MasteryType.Attack:
                        stats.UpgradeAttack(mastery.Attack);
                        break;
                    case MasteryType.Spell:
                        stats.UpgradeSpell(mastery.Spell);
                        break;
                    default:
                        break;
                }
            }

            enemies.Add(stats);
        }

        foreach (var enemy in enemies)
        {
            foreach (var stat in globalBuffs)
            {
                enemy.AddStat(stat);
            }
        }
    }

    void OnBattleStart()
    {
        LoadEnemyBoardData();
        LoadBoardData();

        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        BattleStartCanvas g = Instantiate(battleStartPrefab);
        g.Initialise(playerBoardData, enemyBoardData);

        while (g != null)
            yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(1);

        foreach (var item in allies)
        {
            //5 if its in the back square
            StartCoroutine(MakeCharacterTargetable(item, targetableAllies, 1));
        }

        foreach (var item in enemies)
        {
            //5 if its in the back square
            StartCoroutine(MakeCharacterTargetable(item, targetableEnemies, 1));
        }
    }

    IEnumerator MakeCharacterTargetable(CharacterStats stats, List<CharacterStats> team, float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        team.Add(stats);
    }

    void ClearBoard()
    {
        foreach (var ally in allies)
        {
            if (ally != null)
                Destroy(ally.gameObject);
        }

        foreach (var enemy in enemies)
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }

        allies.Clear();
        enemies.Clear();
        targetableAllies.Clear();
        targetableEnemies.Clear();
    }

    public static void KillCharacter(CharacterStats stats)
    {
        if (allies.Contains(stats))
        {
            allies.Remove(stats);
            targetableAllies.Remove(stats);

            if (allies.Count == 0)
                CoroutineManager.instance.StartCoroutine(EndRound());
        }
        else if (enemies.Contains(stats))
        {
            enemies.Remove(stats);
            targetableEnemies.Remove(stats);

            if (enemies.Count == 0)
                CoroutineManager.instance.StartCoroutine(EndRound());
        }
    }

    static IEnumerator EndRound()
    {
        yield return new WaitForSeconds(3);

        if (allies.Count == 0)
            GameManager.OnRoundLost?.Invoke();
        else
            GameManager.OnRoundWon?.Invoke();

        GameManager.EndRound();
    }
}
