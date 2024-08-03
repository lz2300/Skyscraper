using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


//
public enum AudioType 
{
    Normal,
    Perfect,
}

public class AudioManager : MonoBehaviour
{
    public AudioSource sourceAudio;
    public AudioClip GetClip(AudioType type)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + type.ToString());
        return clip;
    }
    public void AudioPlay(AudioType type)
    {
        sourceAudio.clip = GetClip(type);
        sourceAudio.Play();
    }
}
