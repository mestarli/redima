using System;
using UnityEngine;

[Serializable]

public class Sound
{
    // Variables
    public string name;

    public AudioClip audioClip;

    [Range(0f, 1f)] 
    public float volume;

    public bool loop;
    public bool playOnAwake;

    [HideInInspector] 
    public AudioSource audioSource;
}
