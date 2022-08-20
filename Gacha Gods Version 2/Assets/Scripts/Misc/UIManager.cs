using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject uiCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject teamCanvas;
    [SerializeField] GameObject gachaCanvas;
    [SerializeField] GameObject achievementsCanvas;
    [SerializeField] GameObject characterCanvas;

    [Header("Prefabs")]
    [SerializeField] GameObject roundStartPrefab;

    private void OnEnable()
    {
        GameManager.OnRoundStart += SpawnRoundStartPrefab;
        GameManager.OnRoundStart += ActivateMainCanvas;
        GameManager.OnBattleStart += DeactivateMainCanvas;

        uiCanvas.SafeSetActive(true);
        mainCanvas.SafeSetActive(true);
        teamCanvas.SafeSetActive(false);
        gachaCanvas.SafeSetActive(false);
        achievementsCanvas.SafeSetActive(false);
        characterCanvas.SafeSetActive(false);
    }

    private void OnDisable()
    {
        GameManager.OnRoundStart -= SpawnRoundStartPrefab;
        GameManager.OnRoundStart -= ActivateMainCanvas;
        GameManager.OnBattleStart -= DeactivateMainCanvas;
    }

    void ActivateMainCanvas()
    {
        uiCanvas.SafeSetActive(true);
        mainCanvas.SafeSetActive(true);
    }

    void DeactivateMainCanvas()
    {
        uiCanvas.SafeSetActive(false);
    }

    void SpawnRoundStartPrefab()
    {
        GameObject g = Instantiate(roundStartPrefab);
        g.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText("Day " + GameManager.RoundNumber);
    }
}
