using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    private AudioSource aSource;
    public static BGMManager Instance;

    public Toggle muteToggle;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        aSource = GetComponent<AudioSource>();

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetMuteToggle(Toggle value)
    {
        muteToggle = value;
    }

    public void Mute(bool value)
    {
        aSource.mute = !value;
        GameManager.Instance.BGMMute = !value;
    }

    public void SetMute(bool value)
    {
        aSource.mute = value;
        muteToggle.isOn = !value;
    }
}
