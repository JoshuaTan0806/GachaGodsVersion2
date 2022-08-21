using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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

            allySets[i].gameObject.SetActive(true);

            if (trait as Archetype != null)
                allySets[i].SetText(trait.name + ": " + allyBoard.Archetypes[(Archetype)trait]);
            else
                allySets[i].SetText(trait.name + ": " + allyBoard.Roles[(Role)trait]);
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

            enemySets[i].gameObject.SetActive(true);

            if (trait as Archetype != null)
                enemySets[i].SetText(trait.name + ": " + enemyBoard.Archetypes[(Archetype)trait]);
            else
                enemySets[i].SetText(trait.name + ": " + enemyBoard.Roles[(Role)trait]);
        }
    }
}
