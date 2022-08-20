using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static List<CharacterStats> allies;
    public static List<CharacterStats> enemies;

    public static List<CharacterStats> targetableAllies;
    public static List<CharacterStats> targetableEnemies;

    [SerializeField] List<Transform> spawnPoints;

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

    public void LoadBoardData()
    {
        BoardData boardData = BoardDatabase.PlayerBoard;

        List<StatData> globalBuffs = new List<StatData>();

        foreach (var item in boardData.Archetypes)
        {
            List<StatData> stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.Roles)
        {
            List<StatData> stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = spawnPoints[item.Position].position;
            CharacterStats stats = Instantiate(item.Character.Prefab, spawnPos, Quaternion.identity);

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

            allies.Add(stats);
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
        if(BoardDatabase.DatabaseIsEmpty(GameManager.RoundNumber))
        {
            Board.SaveBoardData();
        }

        BoardData boardData = BoardDatabase.LoadEnemyBoard(GameManager.RoundNumber);

        List<StatData> globalBuffs = new List<StatData>();

        foreach (var item in boardData.Archetypes)
        {
            List<StatData> stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.Roles)
        {
            List<StatData> stats = item.Key.FindStats(item.Value);

            if (stats == null)
                continue;

            foreach (var stat in stats)
            {
                globalBuffs.Add(stat);
            }
        }

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = spawnPoints[spawnPoints.Count - 1 - item.Position].position;
            CharacterStats stats = Instantiate(item.Character.Prefab, spawnPos, Quaternion.identity).GetComponent<CharacterStats>();

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
    }

    void ClearBoard()
    {
        foreach (var ally in allies)
        {
            Destroy(ally.gameObject);
        }

        foreach (var enemy in enemies)
        {
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
        else if(enemies.Contains(stats))
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
