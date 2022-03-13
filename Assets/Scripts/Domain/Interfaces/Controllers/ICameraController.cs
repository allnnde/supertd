using UnityEngine;

public interface ICameraController
{
    void MoveCamera(Vector3 position);

    void ZoomCamera(float zoom);
}