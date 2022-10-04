using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public static List<CharacterStats> allies = new List<CharacterStats>();
    public static List<CharacterStats> enemies = new List<CharacterStats>();

    public static List<CharacterStats> targetableAllies = new List<CharacterStats>();
    public static List<CharacterStats> targetableEnemies = new List<CharacterStats>();

    public static List<Node> availableNodes = new();

    public static BoardData playerBoardData;
    public static BoardData enemyBoardData;

    public static bool battleHasStarted = false;

    [SerializeField] float nodeSize;
    [ReadOnly, SerializeField] List<Transform> spawnPoints;
    [ReadOnly, SerializeField] List<Node> nodes;

    [Header("References")]
    [SerializeField] Transform spawnsReference;
    [SerializeField] Transform nodesReference;

    [Header("Prefabs")]
    [SerializeField] BattleStartCanvas battleStartPrefab;
    [SerializeField] CharacterStats BaseCharacterPrefab;
    [SerializeField] Node nodePrefab;

    private void Awake()
    {
        ClearBoard();
        SpawnBoard();
        battleHasStarted = false;
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

    void SpawnBoard()
    {
        spawnPoints = new();

        //make the spawn points

        //we have to go downwards first
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Vector3 spawnPos = new();
                spawnPos.x = -9 + (i * 2);
                spawnPos.y = 5 - (j * 2);

                GameObject g = new GameObject();
                g.name = "Spawn Point (" + i + ", " + j + ")";
                g.transform.position = spawnPos;
                g.SetParent(spawnsReference);
                spawnPoints.Add(g.transform);
            }
        }

        //make the nodes
        for (int i = 0; i < 23; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                Vector3 spawnPos = new();

                spawnPos.x = -11 + i;
                spawnPos.y = -6 + j;

                Node g = Instantiate(nodePrefab, spawnPos, Quaternion.identity, nodesReference);
                g.name = "Node (" + i + ", " + j + ")";
                g.transform.localScale = Vector3.one * nodeSize;
                nodes.Add(g);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Node node = nodes[i];

            List<Node> neighbours = nodes.Where(x => Vector3.SqrMagnitude(node.transform.position - x.transform.position) <= 2 && x != node).ToList();

            for (int j = 0; j < neighbours.Count; j++)
            {
                float distance = Vector3.Distance(node.transform.position, neighbours[j].transform.position);
                node.AddNeighbour(neighbours[j], distance);
            }
        }
    }

    public static List<CharacterStats> FindAllies(CharacterStats stats)
    {
        if (allies.Contains(stats))
            return targetableAllies;
        else
            return targetableEnemies;
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

        List<StatData> globalBuffs = CharacterManager.FindStatsFromTraits(boardData.Traits);

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = spawnPoints[item.Position].position;
            CharacterStats stats = Instantiate(BaseCharacterPrefab, spawnPos, Quaternion.identity, transform);
            stats.name = "Ally " + item.Character;
            allies.Add(stats);
            stats.InitialiseCharacter(item.Character);

            for (int i = 0; i < item.Mastery; i++)
            {
                Mastery mastery = item.Character.Masteries[i];

                switch (mastery.MasteryType)
                {
                    case MasteryType.Stat:
                        foreach (var stat in mastery.Stats)
                        {
                            globalBuffs.Add(stat);
                        }
                        break;
                    case MasteryType.Trait:
                        foreach (var trait in mastery.Traits)
                        {
                            stats.AddTrait(trait);
                        }
                        break;
                    case MasteryType.Attack:
                        stats.UpgradeAttack(mastery.Attacks);
                        break;
                    case MasteryType.Spell:
                        stats.UpgradeSpell(mastery.Spells);
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

        List<StatData> globalBuffs = CharacterManager.FindStatsFromTraits(boardData.Traits);

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = spawnPoints[spawnPoints.Count - 1 - item.Position].position;
            CharacterStats stats = Instantiate(BaseCharacterPrefab, spawnPos, Quaternion.identity, transform);
            stats.name = "Enemy " + item.Character;
            stats.InitialiseCharacter(item.Character);

            for (int i = 0; i < item.Mastery; i++)
            {
                Mastery mastery = item.Character.Masteries[i];

                switch (mastery.MasteryType)
                {
                    case MasteryType.Stat:
                        foreach (var stat in mastery.Stats)
                        {
                            globalBuffs.Add(stat);
                        }
                        break;
                    case MasteryType.Trait:
                        foreach (var trait in mastery.Traits)
                        {
                            stats.AddTrait(trait);
                        }
                        break;
                    case MasteryType.Attack:
                        stats.UpgradeAttack(mastery.Attacks);
                        break;
                    case MasteryType.Spell:
                        stats.UpgradeSpell(mastery.Spells);
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

        battleHasStarted = true;

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
            CoroutineManager.instance.StartCoroutine(RemoveCharacter(stats));

            if (allies.Count == 0)
                CoroutineManager.instance.StartCoroutine(EndRound());
        }
        else if (enemies.Contains(stats))
        {
            enemies.Remove(stats);
            targetableEnemies.Remove(stats);
            CoroutineManager.instance.StartCoroutine(RemoveCharacter(stats));

            if (enemies.Count == 0)
                CoroutineManager.instance.StartCoroutine(EndRound());
        }
    }

    static IEnumerator RemoveCharacter(CharacterStats stats)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(stats.gameObject);
    }

    static IEnumerator EndRound()
    {
        yield return new WaitForSeconds(3);

        battleHasStarted = false;

        if (allies.Count == 0)
            GameManager.LoseBattle(enemies.Count);
        else
            GameManager.WinBattle();
    }
}
