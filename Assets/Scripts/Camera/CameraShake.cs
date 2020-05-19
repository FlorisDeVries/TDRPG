using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : UnitySingleton<CameraShake>
{
    [SerializeField]
    [Tooltip("How instense the camera shake should be")]
    private float _defaultIntensity = 2f;

    public void Shake(float intensity)
    {
        // Simple one sided shake
        transform.position -= transform.forward * intensity * _defaultIntensity;
    }
}
