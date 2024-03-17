using System;
using UnityEngine;

public class ThirdCameraControl : MonoBehaviour
{
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private float smoothTimeToCameraPoint = 0.1f;

    private Vector3 _smoothDampVelocity;
    
    private void LateUpdate()
    {
        var source = transform;
        source.position = Vector3.SmoothDamp(source.position, cameraPoint.position, ref _smoothDampVelocity,
            smoothTimeToCameraPoint);
        source.rotation = cameraPoint.rotation;
    }
}