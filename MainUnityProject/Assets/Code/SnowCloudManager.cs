using System;
using UnityEngine;

public class SnowCloudManager : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float zOffset;
    
    void FixedUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x + xOffset, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
    }
}
