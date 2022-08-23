using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class EndRoundButton : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddListenerToButton(() => GameManager.EndRound());
    }
}
