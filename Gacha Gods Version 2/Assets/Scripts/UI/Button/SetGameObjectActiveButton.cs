using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class SetGameObjectActiveButton : MonoBehaviour
{
    [SerializeField] GameObject GameObject;
    [SerializeField] bool active = true;

    private void Awake()
    {
        gameObject.AddListenerToButton(() => ToggleGameObject());
    }

    void ToggleGameObject()
    {
        GameObject.SafeSetActive(active);
    }
}
