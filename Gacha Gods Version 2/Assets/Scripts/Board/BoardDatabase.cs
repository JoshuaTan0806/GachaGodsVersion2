using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/BoardDatabase")]
public class BoardDatabase : Factories.FactoryBase
{
    public static Dictionary<int, List<BoardData>> Database => database;
    static Dictionary<int, List<BoardData>> database = new Dictionary<int, List<BoardData>>();
    public static BoardData PlayerBoard;

    public override void Initialise()
    {

    }

    public static void SaveBoard(BoardData boardData)
    {
        PlayerBoard = boardData;

        if (database.ContainsKey(boardData.RoundNumber))
            database[boardData.RoundNumber].Add(boardData);
        else
        {
            List<BoardData> boardDatas = new List<BoardData>();
            boardDatas.Add(boardData);
            database.Add(boardData.RoundNumber, boardDatas);
        }
    }

    public static BoardData LoadEnemyBoard(int roundNumber)
    {
        if (DatabaseIsEmpty(roundNumber))
            throw new System.Exception("Database for round " + roundNumber + " is empty.");

        return database[roundNumber].ChooseRandomElementInList();
    }

    public static bool DatabaseIsEmpty(int roundNumber)
    {
        if (!database.ContainsKey(roundNumber))
            return true;

        if (database[roundNumber].IsNullOrEmpty())
            return true;

        return false;
    }

    [Button]
    public void ClearDatabase()
    {
        database.Clear();
    }
}
