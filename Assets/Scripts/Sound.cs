using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name="";
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume=0.1f;
    [Range(0.1f, 3f)] public float pitch=1;
    public bool loop=false;
    public bool playOnAwake=false;
}
