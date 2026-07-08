using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] AudioClipContainer[] sounds;
    [SerializeField] AudioSource source;
    [SerializeField] Transform playerPos;
    [SerializeField] float minRange = 20f;
    [SerializeField] float maxRange = 70f;
    [SerializeField] float minTimeBetweenAmbientNoise = 20f;
    [SerializeField] float maxTimeBetweenAmbientNoise = 80f;
    float timer;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        timer = Random.Range(minTimeBetweenAmbientNoise, maxTimeBetweenAmbientNoise);
    }

    void Update()
    {
        timer = Math.Clamp(timer - Time.deltaTime, 0, maxTimeBetweenAmbientNoise);
        if (timer <= 0)
        {
            float angle = Random.Range(0, 360);
            float radius = Random.Range(minRange, maxRange);
            transform.position = ReturnPointAroundPlayer(playerPos.position, angle, radius);
            source.resource = sounds[Random.Range(0, sounds.Length)].AudioClip;
            source.Play();
            timer = Random.Range(minTimeBetweenAmbientNoise, maxTimeBetweenAmbientNoise);

        }
    }
    private Vector3 ReturnPointAroundPlayer(Vector3 pointToRotateAround, float angle, float radius)
    {
        return new Vector3(pointToRotateAround.x + (math.cos(angle) * radius), pointToRotateAround.y,
            pointToRotateAround.z + (math.sin(angle) * radius));
    }
}
[Serializable]
public class AudioClipContainer
{
    public AudioResource AudioClip;
    public string Name;
}

