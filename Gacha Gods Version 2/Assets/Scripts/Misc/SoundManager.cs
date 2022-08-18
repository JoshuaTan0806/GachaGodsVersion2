using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Componenets")]
    public static AudioSource SFXAudio;
    public static AudioSource BGMAudio;

    [SerializeField] AudioSource sfxAudio;
    [SerializeField] AudioSource bgmAudio;

    [Header("SFX")]
    public static AudioClip ConfirmButtonSFX;
    public static AudioClip CancelButtonSFX;

    [SerializeField] AudioClip confirmButtonSFX;
    [SerializeField] AudioClip cancelButtonSFX;

    [Header("BGM")]
    public static AudioClip MainMenuBGM;
    [SerializeField] AudioClip mainMenuBGM;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SFXAudio = sfxAudio;
        BGMAudio = bgmAudio;
        ConfirmButtonSFX = confirmButtonSFX;
        CancelButtonSFX = cancelButtonSFX;
    }

    public static void PlaySFX(AudioClip clip)
    {
        SFXAudio.PlayOneShot(clip);
    }

    public static void PlayBGM(AudioClip clip)
    {
        BGMAudio.clip = clip;
        BGMAudio.Play();
    }

    public static void PlayConfirmButton()
    {
        PlaySFX(ConfirmButtonSFX);
    }

    public static void PlayCancelButton()
    {
        PlaySFX(CancelButtonSFX);
    }
}
