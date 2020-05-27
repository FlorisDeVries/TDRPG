using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraSmoothFollow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The transform to follow in the scene")]
    private Transform _target = default;

    [SerializeField]
    [Tooltip("How smooth/fast we should follow the target")]
    private float _smoothStep = 0.125f;

    [SerializeField]
    [Tooltip("Offset of position, relative position to the target on our plane")]
    private Vector3 _offset = default;

    // Update is called once per frame
    void Update()
    {
        // Smooth follow the position
        Vector3 desiredPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, _smoothStep * Time.deltaTime);

        if (!Application.isPlaying)
            transform.position = desiredPos;

        transform.LookAt(_target);
    }
}
