using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Transition : MonoBehaviour
{
    Image image;
    bool hasFiredEvents = false;
    UnityEvent unityEvent;
    Action action;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    public void Initialise(UnityEvent unityEvent)
    {
        this.unityEvent = unityEvent;
    }

    public void Initialise(Action action)
    {
        this.action += action;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireEvents();
            Destroy(gameObject);
        }

        if(!hasFiredEvents)
        {
            if (!image.IsOpaque())
                image.DecreaseTransparency(5f);
            else
            {
                FireEvents();
            }
        }
        else
        {
            if (!image.IsTransparent())
                image.IncreaseTransparency(5f);
            else
                Destroy(gameObject);
        }
    }

    void FireEvents()
    {
        unityEvent?.Invoke();
        action?.Invoke();
        hasFiredEvents = true;
    }
}
