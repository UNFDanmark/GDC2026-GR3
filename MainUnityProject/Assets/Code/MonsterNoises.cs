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
    void Start()
    {
        if (AmbientSource == null)
        {
            AmbientSource = GetComponent<AudioSource>();
        }
    }
    [ContextMenu("test")]
    public void test()
    {
        PlayAmbientNoise("DeepScreams");
    }

    void Update()
    {
        cooldownAmbientScreams = Math.Clamp(cooldownAmbientScreams - Time.deltaTime, 0, maxTimeBetweenAmbientScreams);

        if (cooldownAmbientScreams <= 0)
        {
            cooldownAmbientScreams = Random.Range(minTimeBetweenAmbientScreams, maxTimeBetweenAmbientScreams);
            PlayAmbientNoise(AmbientScreams[Random.Range(0, AmbientScreams.Length)]);
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
