using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    [Range(0.01f, 1.0f)]
    [SerializeField] private float smoothness = 0.5f;

    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        Vector3 newCameraPos = player.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newCameraPos, smoothness);
    }
}
