using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static List<Tile> Tiles;
    [SerializeField] List<Tile> tiles;
    [SerializeField] Button viewTraitsButton;
    [SerializeField] GameObject traitsPreviewPrefab;

    private void Awake()
    {
        Tiles = tiles;
        viewTraitsButton.AddListenerToButton(() => ViewTraits());
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

        return new BoardData(GameManager.RoundNumber, characterDatas, CharacterManager.ActiveTraits);
    }

    public static void SaveBoardData()
    {
        BoardDatabase.SaveBoard(PlayerBoard());
    }

    void ViewTraits()
    {
        Instantiate(traitsPreviewPrefab);
    }
}
