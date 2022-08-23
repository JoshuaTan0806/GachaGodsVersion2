using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float timeToHold;
    bool isBeingHovered = false;
    public System.Action OnHeld;
    bool isBeingHeld;
    float timer = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isBeingHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isBeingHeld = false;
        isBeingHovered = false;
        timer = timeToHold;
    }

    private void Awake()
    {
        timer = timeToHold;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBeingHovered)
            isBeingHeld = true;
        else if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
            timer = timeToHold;
        }

        if(isBeingHeld)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
                Execute();
        }
    }

    void Execute()
    {
        OnHeld?.Invoke();
        isBeingHovered = false;
        timer = timeToHold;
    }
}
