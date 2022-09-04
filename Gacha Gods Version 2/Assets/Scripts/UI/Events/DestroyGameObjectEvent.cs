using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectEvent : MonoBehaviour
{
    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
