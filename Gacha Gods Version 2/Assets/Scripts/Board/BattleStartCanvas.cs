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
        List<ScriptableObject> traits = new List<ScriptableObject>();

        foreach (var item in allyBoard.Archetypes)
        {
            traits.Add(item.Key);
        }

        foreach (var item in allyBoard.Roles)
        {
            traits.Add(item.Key);
        }

        traits = traits.OrderByDescending(x => x as Archetype != null ? allyBoard.Archetypes[(Archetype)x] : allyBoard.Roles[(Role)x]).ToList();

        for (int i = 0; i < traits.Count; i++)
        {
            ScriptableObject trait = traits[i];

            TextMeshProUGUI text = allySetsHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.gameObject.SetActive(true);

            if (trait as Archetype != null)
                text.SetText(trait.name + ": " + allyBoard.Archetypes[(Archetype)trait]);
            else
                text.SetText(trait.name + ": " + allyBoard.Roles[(Role)trait]);
        }
    }

    void SetEnemyTraits(BoardData enemyBoard)
    {
        List<ScriptableObject> traits = new List<ScriptableObject>();

        traits = new List<ScriptableObject>();

        foreach (var item in enemyBoard.Archetypes)
        {
            traits.Add(item.Key);
        }

        foreach (var item in enemyBoard.Roles)
        {
            traits.Add(item.Key);
        }

        traits = traits.OrderByDescending(x => x as Archetype != null ? enemyBoard.Archetypes[(Archetype)x] : enemyBoard.Roles[(Role)x]).ToList();

        for (int i = 0; i < traits.Count; i++)
        {
            ScriptableObject trait = traits[i];

            TextMeshProUGUI text = enemySetsHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.gameObject.SetActive(true);

            if (trait as Archetype != null)
                text.SetText(trait.name + ": " + enemyBoard.Archetypes[(Archetype)trait]);
            else
                text.SetText(trait.name + ": " + enemyBoard.Roles[(Role)trait]);
        }
    }
}
