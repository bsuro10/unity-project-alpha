using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{

    [SerializeField] private float zoomSpeed = 35;
    [SerializeField] private float maxZoom = 60;
    [SerializeField] private float minZoom = 30;

    private Camera cam;
    private float camFov;
    private float mouseScrollInput;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();

    }
    void Start()
    {
        camFov = cam.fieldOfView;
    }

    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        camFov -= mouseScrollInput * zoomSpeed;
        camFov = Mathf.Clamp(camFov, minZoom, maxZoom);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFov, zoomSpeed);
    }
}
