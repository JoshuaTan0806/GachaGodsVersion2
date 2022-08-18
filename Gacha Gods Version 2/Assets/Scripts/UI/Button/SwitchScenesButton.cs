using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class SwitchScenesButton : MonoBehaviour
{
    [SerializeField] int SceneNumber;

    private void Awake()
    {
        gameObject.AddListenerToButton(() => SwitchScenes());
    }

    void SwitchScenes()
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
