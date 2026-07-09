using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MonsterNoises : MonoBehaviour
{
    [Header("Sounds")]
    [FormerlySerializedAs("sounds")] [SerializeField] AudioClipContainer[] Sounds;
    
    [Header("Ambient screams")]
    [FormerlySerializedAs("Source")][FormerlySerializedAs("source")] [SerializeField] AudioSource AmbientSource;
    [SerializeField] string[] AmbientScreams;
    [SerializeField] float maxTimeBetweenAmbientScreams = 120;
    [SerializeField] float minTimeBetweenAmbientScreams = 20;
    [Min(0)][SerializeField]float cooldownAmbientScreams;

    [Header("Hunting noises")] 
    [SerializeField] AudioSource huntingAmbience;
    [SerializeField] AudioSource huntingScreamSource;
    [SerializeField] string[] huntingAmbiences;
    [SerializeField] string[] huntingScreams;
    [SerializeField] string[] huntStartScreams;
    [SerializeField] float maxTimeBetweenHuntScreams = 8f;
    [SerializeField] float minTimeBetweenHuntScreams = 3f;
    [Min(0)] [SerializeField] float cooldownHuntScream;
    bool activeHunt;
    void Start()
    {
        cooldownAmbientScreams = Random.Range(minTimeBetweenAmbientScreams, maxTimeBetweenAmbientScreams);
        cooldownHuntScream = Random.Range(minTimeBetweenHuntScreams, maxTimeBetweenHuntScreams);
    }
    [ContextMenu("test")]
    public void test()
    {
        PlayAmbientNoise("DeepScreams");
    }

    void Update()
    {
        cooldownAmbientScreams = Math.Clamp(cooldownAmbientScreams - Time.deltaTime, 0, maxTimeBetweenAmbientScreams);
        cooldownHuntScream = Math.Clamp(cooldownHuntScream - Time.deltaTime, 0, maxTimeBetweenHuntScreams);
        if (cooldownAmbientScreams <= 0)
        {
            cooldownAmbientScreams = Random.Range(minTimeBetweenAmbientScreams, maxTimeBetweenAmbientScreams);
            PlayAmbientNoise(AmbientScreams[Random.Range(0, AmbientScreams.Length)]);
        }

        if (activeHunt)
        {

            if (cooldownHuntScream <= 0)
            {
                cooldownHuntScream = Random.Range(minTimeBetweenHuntScreams, maxTimeBetweenHuntScreams);
                PlayHuntingScream(huntingScreams[Random.Range(0, huntingScreams.Length)]);
            }
        }
    }

    //THIS ONE FOR START HUNT AND THEN IT LOOPS
    public void StartHuntAmbience()
    {
        if (activeHunt)
        {
            return;
        }
        if (cooldownHuntScream > 0)
            return;
        cooldownHuntScream = Random.Range(minTimeBetweenHuntScreams, maxTimeBetweenHuntScreams);
        Debug.Log("HUNT MUSIC SHOULD BE PLAYING");
        activeHunt = true;
        foreach (var noise in Sounds)
        {
            
            if (noise.Name == huntStartScreams[Random.Range(0, huntStartScreams.Length)])
            {
                huntingScreamSource.resource = noise.AudioClip;
                huntingScreamSource.Play();
            }
        }
        foreach (var noise in Sounds)
        {
            if (noise.Name == huntingAmbiences[Random.Range(0, huntingAmbiences.Length)])
            {
                huntingAmbience.resource = noise.AudioClip;
                huntingAmbience.Play();
            }
        }
    }

    public void EndHuntAmbience()
    {
        activeHunt = false;
        huntingAmbience.Stop();
    }
    public void PlayHuntingScream(string name)
    {
        foreach (var noise in Sounds)
        {
            if (name == noise.Name)
            {
                huntingScreamSource.resource = noise.AudioClip;
                huntingScreamSource.Play();
            }
        }
    }
    public void PlayAmbientNoise(string name)
    {
        foreach (var noise in Sounds)
        {
            if (name == noise.Name)
            {
                AmbientSource.resource = noise.AudioClip;
                AmbientSource.Play();
            }
        }
    }
}
