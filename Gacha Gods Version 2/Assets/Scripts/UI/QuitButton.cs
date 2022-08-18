using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class QuitButton : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddListenerToButton(() => QuitGame());
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
