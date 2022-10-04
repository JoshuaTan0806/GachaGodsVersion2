using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    static Transition TransitionPrefab;
    [SerializeField] Transition transitionPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        TransitionPrefab = transitionPrefab;
    }

    public static void SpawnTransition(UnityEvent unityEvent)
    {
        Transition t = Instantiate(TransitionPrefab);
        t.Initialise(unityEvent);
    }

    public static void SpawnTransition(Action action)
    {
        Transition t = Instantiate(TransitionPrefab);
        t.Initialise(action);
    }
}
