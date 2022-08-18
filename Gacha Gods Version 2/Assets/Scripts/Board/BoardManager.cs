using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;
    [SerializeField] Vector2 startingPos;
    [SerializeField] int width;
    [SerializeField] int height;

    static LayerMask WhatIsTile;
    [SerializeField] LayerMask whatIsTile;

    Tile[,] board;

    public static CharacterStats HeldCharacter
    {
        get
        {
            return heldCharacter;
        }
        set
        {
            heldCharacter = value;
            OnHeldCharacterChanged?.Invoke();
        }
    }
    static CharacterStats heldCharacter;
    public static System.Action OnHeldCharacterChanged;

    public static List<Transform> Allies => allies;
    static List<Transform> allies = new List<Transform>();
    public static List<Transform> Enemies => enemies;
    static List<Transform> enemies = new List<Transform>();

    [Header("References")]
    [SerializeField] Transform tilesReference;
    [SerializeField] Transform alliesReference;
    [SerializeField] Transform enemiesReference;

    private void Awake()
    {
        allies = new List<Transform>();
        enemies = new List<Transform>();
        WhatIsTile = whatIsTile;
        SpawnBoard();

        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnRoundEnd += OnRoundEnd;
    }

    private void OnDestroy()
    {
        GameManager.OnRoundStart -= OnRoundStart;
        GameManager.OnRoundEnd -= OnRoundEnd;
    }

    void SpawnBoard()
    {
        board = new Tile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                board[i, j] = Instantiate(tilePrefab, new Vector3(startingPos.x + i, 0, startingPos.y + j), Quaternion.identity, tilesReference);

                if (i >= width / 2)
                {
                    if (i == width - 1)
                        board[i, j].SetUnitType(UnitType.Assassin);
                    else
                        board[i, j].SetUnitType(UnitType.None);
                }
                else
                {
                    if (i == 0)
                        board[i, j].SetUnitType(UnitType.None);
                    else
                        board[i, j].SetUnitType(UnitType.All);
                }
            }
        }
    }

    private void Update()
    {
        HeldCharacterFollowMouse();
        CheckForClick();
    }

    void HeldCharacterFollowMouse()
    {
        if (HeldCharacter != null)
        {
            if (HeldCharacter.transform.parent != alliesReference)
                HeldCharacter.transform.SetParent(alliesReference);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -Camera.main.transform.position.z;
            HeldCharacter.transform.position = pos;
        }
    }

    void CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = RaycastTile();

            if (tile == null)
                return;

            if (HeldCharacter != null)
            {
                PlaceCharacter(tile);
            }
            else
            {
                PickUpCharacter(tile);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (heldCharacter != null)
                return;

            Tile tile = RaycastTile();

            if (tile == null)
                return;

            RemoveCharacter(tile);
        }
    }

    static void PickUpCharacter(Tile tile)
    {
        CharacterStats characterStats = tile.PickUpCharacter();

        if (characterStats == null)
            return;

        HeldCharacter = characterStats;
    }

    static void PlaceCharacter(Tile tile)
    {
        if (!tile.CanBePlaced(HeldCharacter.Character))
            return;

        CharacterStats stats = tile.PickUpCharacter();

        HeldCharacter.transform.position = tile.transform.position;
        tile.PlaceCharacter(HeldCharacter);

        //pick up existing character
        HeldCharacter = stats;
    }

    static void RemoveCharacter(Tile tile)
    {
        CharacterStats characterStats = tile.Character;

        if (characterStats == null)
            return;

        Destroy(tile.Character.gameObject);
        CharacterManager.DeactivateCharacter(characterStats.Character);
        HeldCharacter = null;
    }

    static Tile RaycastTile()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -Camera.main.transform.position.z;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, WhatIsTile);

        if (hit.collider == null)
            return null;

        return hit.transform.GetComponent<Tile>();
    }

    [Button]
    public void LoadBoardData()
    {
        BoardData boardData = BoardDatabase.LoadBoard();

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = board[item.Position.x, item.Position.y].transform.position;
            CharacterStats stats = Instantiate(item.Character.Prefab, spawnPos, Quaternion.identity, alliesReference).GetComponent<CharacterStats>();
            board[item.Position.x, item.Position.y].PlaceCharacter(stats);
            stats.UpgradeAttack(item.Attack);
            stats.UpgradeSpell(item.Spell);
            stats.SetStats(item.Stats);
        }
    }

    [Button]
    public void LoadEnemyBoardData()
    {
        BoardData boardData = BoardDatabase.LoadEnemyBoard(GameManager.RoundNumber);

        foreach (var item in boardData.CharacterDatas)
        {
            Vector3 spawnPos = board[width - 1 - item.Position.x, item.Position.y].transform.position;
            CharacterStats stats = Instantiate(item.Character.Prefab, spawnPos, Quaternion.identity, enemiesReference).GetComponent<CharacterStats>();
            stats.UpgradeAttack(item.Attack);
            stats.UpgradeSpell(item.Spell);
            stats.SetStats(item.Stats);
            //stats.GetComponent<AI>().SetAsEnemy();
        }
    }

    [Button]
    public void SaveBoardData()
    {
        List<CharacterData> characterDatas = new List<CharacterData>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (board[i, j].Character == null)
                    continue;

                CharacterStats character = board[i, j].Character;

                CharacterData characterData = new CharacterData(character.Character, CharacterManager.CharacterMastery[character.Character], character.Attack, character.Spell, character.Stats, new Vector2Int(i, j));
                characterDatas.Add(characterData);
            }
        }

        BoardData boardData = new BoardData(GameManager.RoundNumber, characterDatas, CharacterManager.ActiveRoles, CharacterManager.ActiveArchetypes);
        BoardDatabase.SaveBoard(boardData);
    }

    [Button]
    void ClearBoard()
    {
        alliesReference.DestroyAllChildren();
        enemiesReference.DestroyAllChildren();
    }

    void PopulateAlliesAndEnemies()
    {
        allies = new List<Transform>();
        enemies = new List<Transform>();

        for (int i = 0; i < alliesReference.childCount; i++)
        {
            StartCoroutine(PopulateCharacter(alliesReference.GetChild(i), allies));
        }

        for (int i = 0; i < enemiesReference.childCount; i++)
        {
            StartCoroutine(PopulateCharacter(enemiesReference.GetChild(i), enemies));
        }
    }

    IEnumerator PopulateCharacter(Transform character, List<Transform> holder)
    {
        if (character.GetComponent<CharacterStats>().Character.Roles.Contains(CharacterManager.Assassin))
            yield return new WaitForSeconds(5);
        else
            yield return new WaitForSeconds(3);

        holder.Add(character);
    }

    void OnRoundStart()
    {
        if (BoardDatabase.DatabaseIsEmpty(GameManager.RoundNumber))
        {
            SaveBoardData();
            LoadEnemyBoardData();
        }
        else
        {
            LoadEnemyBoardData();
            SaveBoardData();
        }

        PopulateAlliesAndEnemies();
    }

    void OnRoundEnd()
    {
        ClearBoard();
        LoadBoardData();
    }
}