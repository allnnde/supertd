using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    public Camera cam;
    public float maxZoom = 5;
    public float minZoom = 20;
    public float sensitivity = 1;
    public float speed = 30;
    private float targetZoom;

    private ICameraService _cameraService;

    public void Configuration(ICameraService cameraService)
    {
        _cameraService = cameraService;
    }

    void Update()
    {
        targetZoom = _cameraService.ZoomCamera(Input.mouseScrollDelta.y, targetZoom, sensitivity, minZoom, maxZoom, speed, cam.orthographicSize, Time.deltaTime);
        _cameraService.MoveCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), speed, Time.deltaTime);
    }

    void ICameraController.ZoomCamera(float newSize)
    {
        cam.orthographicSize = newSize;
    }

    void ICameraController.MoveCamera(Vector3 position)
    {
        transform.Translate(position);
    }
}