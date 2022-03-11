using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraService : ICameraService
{
    private readonly ICameraController _cameraController;

    public CameraService(ICameraController cameraController)
    {
        _cameraController = cameraController;
    }

    public void MoveCamera(float x, float y, float speed, float time)
    {
        var position = new Vector3(x, y, 0) * speed * time;
        _cameraController.MoveCamera(position);
    }

    public float ZoomCamera(float mouseScrollDeltaY,
                           float targetZoom,
                           float sensitivity,
                           float minZoom,
                           float maxZoom,
                           float speed,
                           float orthographicSize,
                           float time)
    {
        targetZoom -= mouseScrollDeltaY * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        float newSize = Mathf.MoveTowards(orthographicSize, targetZoom, speed * time);
        _cameraController.ZoomCamera(newSize);
        return targetZoom;
    }
}