using System;
using UnityEngine;

public class Stick : MonoBehaviour
{
    MonsterAI monster;
    float timer = 10f;
    void OnEnable()
    {
        monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterAI>();
        monster.StickActive = true;
    }

    void Update()
    {
        timer = timer - Time.deltaTime;
        monster.StickPosition = transform;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        monster.StickActive = false;
        monster.StickPosition = null;
    }
}
