using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] SoundContainer[] sounds;

    void Start()
    {
        foreach (var sound in sounds)
        {
            AudioSource.PlayClipAtPoint(sound.AudioClip, new Vector3(7.46000004f,3.02999997f,5.53000021f));
        }
    }
}
[Serializable]
public class SoundContainer
{
    public AudioClip AudioClip;
    public string Name;
}

