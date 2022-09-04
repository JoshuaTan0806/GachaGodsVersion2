using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{
    Image image;
    bool hasFiredEvents = false;
    UnityEvent unityEvent;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    public void Initialise(UnityEvent unityEvent)
    {
        this.unityEvent = unityEvent;
    }

    private void Update()
    {
        if(!hasFiredEvents)
        {
            if (!image.IsOpaque())
                image.DecreaseTransparency(5f);
            else
            {
                unityEvent?.Invoke();
                hasFiredEvents = true;
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
}
