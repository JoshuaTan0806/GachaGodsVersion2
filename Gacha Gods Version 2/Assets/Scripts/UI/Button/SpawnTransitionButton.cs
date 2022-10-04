using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnTransitionButton : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;

    private void Awake()
    {
        gameObject.AddListenerToButton(() => SpawnTransition());
    }

    void SpawnTransition()
    {
        TransitionManager.SpawnTransition(unityEvent);
    }
}
