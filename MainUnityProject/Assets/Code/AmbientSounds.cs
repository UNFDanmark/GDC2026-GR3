using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] SoundContainer[] sounds;

}
[Serializable]
public class SoundContainer
{
    public AudioClip AudioClip;
    public string Name;
}

