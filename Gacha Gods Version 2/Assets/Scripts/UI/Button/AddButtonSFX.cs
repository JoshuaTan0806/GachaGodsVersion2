using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonSFX : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    private void Awake()
    {
        gameObject.AddListenerToButton(PlaySound);
    }

    void PlaySound()
    {
        SoundManager.PlaySFX(clip);
    }
}
