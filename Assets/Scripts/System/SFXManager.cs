using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance = null;
    private AudioSource aSource;
    public AudioClip[] clips;

    public Toggle muteToggle;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        aSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string audioName)
    {
        for (int i = 0; i < clips.Length; ++i)
        {
            if (clips[i].name == audioName)
            {
                aSource.clip = clips[i];
                aSource.pitch = 1;
                aSource.Play();
            }
        }
    }

    public void PlaySound(string audioName, float pitch)
    {
        for (int i = 0; i < clips.Length; ++i)
        {
            if (clips[i].name == audioName)
            {
                aSource.clip = clips[i];
                aSource.pitch = pitch;
                aSource.Play();
            }
        }
    }

    public AudioSource GetAudioSource()
    {
        return aSource;
    }

    public void SetMuteToggle(Toggle value)
    {
        muteToggle = value;
    }

    public void Mute(bool value)
    {
        aSource.mute = !value;
        GameManager.Instance.SFXMute = !value;
    }

    public void SetMute(bool value)
    {
        aSource.mute = value;
        muteToggle.isOn = !value;
    }
}
