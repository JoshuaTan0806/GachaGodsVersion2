using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseRandomTip : MonoBehaviour
{
    public List<string> tips;

    private void Awake()
    {
        TextMeshProUGUI label = GetComponent<TextMeshProUGUI>();

        label.SetText(tips.ChooseRandomElementInList());
    }
}
