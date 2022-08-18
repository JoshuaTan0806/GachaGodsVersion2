using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
    [SerializeField] GameObject GameObjectToDestroy;

    private void Awake()
    {
       gameObject.AddListenerToButton(DestroyGameObject);
    }

    void DestroyGameObject()
    {
        if (GameObjectToDestroy == null)
            Destroy(gameObject);
        else
            Destroy(GameObjectToDestroy);
    }
}
