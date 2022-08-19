using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] List<Tile> tiles;

    private void Awake()
    {
        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnRoundEnd += OnRoundEnd;
    }

    private void OnDestroy()
    {
        GameManager.OnRoundStart -= OnRoundStart;
        GameManager.OnRoundEnd -= OnRoundEnd;
    }

    public void LoadBoardData()
    {
        //BoardData boardData = BoardDatabase.LoadBoard();

        //foreach (var item in boardData.CharacterDatas)
        //{
        //    Vector3 spawnPos = board[item.Position.x, item.Position.y].transform.position;
        //    CharacterStats stats = Instantiate(item.Character.Prefab, spawnPos, Quaternion.identity, alliesReference).GetComponent<CharacterStats>();
        //    board[item.Position.x, item.Position.y].PlaceCharacter(stats);
        //    stats.UpgradeAttack(item.Attack);
        //    stats.UpgradeSpell(item.Spell);
        //    stats.SetStats(item.Stats);
        //}

        //load global buffs
    }

    public void LoadEnemyBoardData()
    {
        //BoardData boardData = BoardDatabase.LoadEnemyBoard(GameManager.RoundNumber);

        //foreach (var item in boardData.CharacterDatas)
        //{
        //    Vector3 spawnPos = board[width - 1 - item.Position.x, item.Position.y].transform.position;
        //    CharacterStats stats = Instantiate(item.Character.Prefab, spawnPos, Quaternion.identity, enemiesReference).GetComponent<CharacterStats>();
        //    stats.UpgradeAttack(item.Attack);
        //    stats.UpgradeSpell(item.Spell);
        //    stats.SetStats(item.Stats);
        //    stats.GetComponent<AI>().SetAsEnemy();
        //}

        //load global buffs
    }

    public void SaveBoardData()
    {
        List<CharacterData> characterDatas = new List<CharacterData>();

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].Character == null)
                continue;

            Character character = tiles[i].Character;

            CharacterData characterData = new CharacterData(character, CharacterManager.CharacterMastery[character], i);
            characterDatas.Add(characterData);
        }

        BoardData boardData = new BoardData(GameManager.RoundNumber, characterDatas, CharacterManager.ActiveRoles, CharacterManager.ActiveArchetypes);
        BoardDatabase.SaveBoard(boardData);
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
    }

    void OnRoundEnd()
    {
        LoadBoardData();
    }
}
