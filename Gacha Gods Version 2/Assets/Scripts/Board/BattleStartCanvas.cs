using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class BattleStartCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI allyTeamName;
    [SerializeField] TextMeshProUGUI enemyTeamName;
    [SerializeField] Transform allyCharactersHolder;
    [SerializeField] Transform enemyCharactersHolder;
    [SerializeField] Transform allySetsHolder;
    [SerializeField] Transform enemySetsHolder;

    public void Initialise(BoardData allyBoard, BoardData enemyBoard)
    {
        allyTeamName.SetText("Your Team");
        enemyTeamName.SetText("Enemy Team");

        for (int i = 0; i < allyBoard.CharacterDatas.Count; i++)
        {
            allyCharactersHolder.GetChild(i).gameObject.SetActive(true);

            TextMeshProUGUI text = allyCharactersHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.SetText(allyBoard.CharacterDatas[i].Character.name + " (" + allyBoard.CharacterDatas[i].Mastery + ")");
        }

        for (int i = 0; i < enemyBoard.CharacterDatas.Count; i++)
        {
            enemyCharactersHolder.GetChild(i).gameObject.SetActive(true);

            TextMeshProUGUI text = enemyCharactersHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.SetText(enemyBoard.CharacterDatas[i].Character.name + " (" + enemyBoard.CharacterDatas[i].Mastery + ")");
        }

        SetAllyTraits(allyBoard);
        SetEnemyTraits(enemyBoard);
    }

    void SetAllyTraits(BoardData allyBoard)
    {
        List<Trait> traits = new List<Trait>();

        foreach (var item in allyBoard.Traits)
        {
            traits.Add(item.Key);
        }

        traits = traits.OrderByDescending(x => allyBoard.Traits[x]).ToList();

        for (int i = 0; i < traits.Count; i++)
        {
            Trait trait = traits[i];

            TextMeshProUGUI text = allySetsHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.gameObject.SetActive(true);

            text.SetText(trait.name + ": " + allyBoard.Traits[trait]);
        }
    }

    void SetEnemyTraits(BoardData enemyBoard)
    {
        List<Trait> traits = new List<Trait>();

        traits = new List<Trait>();

        foreach (var item in enemyBoard.Traits)
        {
            traits.Add(item.Key);
        }

        traits = traits.OrderByDescending(x => enemyBoard.Traits[x]).ToList();

        for (int i = 0; i < traits.Count; i++)
        {
            Trait trait = traits[i];

            TextMeshProUGUI text = allySetsHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.gameObject.SetActive(true);

            text.SetText(trait.name + ": " + enemyBoard.Traits[trait]);
        }
    }
}
