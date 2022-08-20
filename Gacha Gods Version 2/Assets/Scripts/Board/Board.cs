using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static List<Tile> Tiles;
    [SerializeField] List<Tile> tiles;

    private void Awake()
    {
        Tiles = tiles;
    }

    private void OnEnable()
    {
        GameManager.OnRoundEnd += SaveBoardData;
    }

    private void OnDisable()
    {
        GameManager.OnRoundEnd -= SaveBoardData;
    }

    public static BoardData PlayerBoard()
    {
        List<CharacterData> characterDatas = new List<CharacterData>();

        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].Character == null)
                continue;

            Character character = Tiles[i].Character;

            CharacterData characterData = new CharacterData(character, CharacterManager.CharacterMastery[character], i);
            characterDatas.Add(characterData);
        }

        return new BoardData(GameManager.RoundNumber, characterDatas, CharacterManager.ActiveRoles, CharacterManager.ActiveArchetypes);
    }

    public static void SaveBoardData()
    {
        BoardDatabase.SaveBoard(PlayerBoard());
    }
}
