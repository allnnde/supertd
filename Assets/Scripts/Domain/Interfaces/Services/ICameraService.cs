using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ICameraService
{
    void MoveCamera(float x, float y, float speed, float time);

    float ZoomCamera(float mouseScrollDeltaY,
                           float targetZoom,
                           float sensitivity,
                           float minZoom,
                           float maxZoom,
                           float speed,
                           float orthographicSize,
                           float time);
}