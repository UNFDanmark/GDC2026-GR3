using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] AudioClipContainer[] sounds;

}
[Serializable]
public class AudioClipContainer
{
    public AudioResource AudioClip;
    public string Name;
}

