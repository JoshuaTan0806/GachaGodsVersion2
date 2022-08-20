using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleStartCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI allyTeamName;
    [SerializeField] TextMeshProUGUI enemyTeamName;
    [SerializeField] List<TextMeshProUGUI> allyCharacters;
    [SerializeField] List<TextMeshProUGUI> enemyCharacters;
    [SerializeField] List<TextMeshProUGUI> allySets;
    [SerializeField] List<TextMeshProUGUI> enemySets;

    public void Initialise(BoardData allyBoard, BoardData enemyBoard)
    {
        allyTeamName.SetText("Your Team");
        enemyTeamName.SetText("Enemy Team");

        for (int i = 0; i < allyBoard.CharacterDatas.Count; i++)
        {
            allyCharacters[i].gameObject.SetActive(true);
            allyCharacters[i].SetText(allyBoard.CharacterDatas[i].Character.name + " (" + allyBoard.CharacterDatas[i].Mastery + ")");
        }

        for (int i = 0; i < enemyBoard.CharacterDatas.Count; i++)
        {
            enemyCharacters[i].gameObject.SetActive(true);
            enemyCharacters[i].SetText(enemyBoard.CharacterDatas[i].Character.name + " (" + enemyBoard.CharacterDatas[i].Mastery + ")");
        }

        //ally sets

        List<string> allySetNames = new List<string>();
        foreach (var item in allyBoard.Archetypes)
        {
            allySetNames.Add(item.Key.name + " (" + item.Value + ")");
        }
        foreach (var item in allyBoard.Roles)
        {
            allySetNames.Add(item.Key.name + " (" + item.Value + ")");
        }

        for (int i = 0; i < allySetNames.Count; i++)
        {
            allySets[i].gameObject.SetActive(true);
            allySets[i].SetText(allySetNames[i]);
        }

        //enemy sets

        List<string> enemySetNames = new List<string>();
        foreach (var item in enemyBoard.Archetypes)
        {
            enemySetNames.Add(item.Key.name + " (" + item.Value + ")");
        }
        foreach (var item in enemyBoard.Roles)
        {
            enemySetNames.Add(item.Key.name + " (" + item.Value + ")");
        }

        for (int i = 0; i < enemySetNames.Count; i++)
        {
            enemySets[i].gameObject.SetActive(true);
            enemySets[i].SetText(enemySetNames[i]);
        }
    }
}
