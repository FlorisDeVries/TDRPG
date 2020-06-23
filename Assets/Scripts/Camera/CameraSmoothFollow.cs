using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private float _height = 0;
    [SerializeField]
    private float _radius = 0;

    private void Start()
    {
        _height = _offset.y;
        _radius = Mathf.Sqrt(Mathf.Pow(_offset.z, 2) + Mathf.Pow(_offset.x, 2));
    }

    void Update()
    {
        // Smooth follow the position
        Vector3 desiredPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, _smoothStep * Time.deltaTime);

        if (!Application.isPlaying)
            transform.position = desiredPos;

        transform.LookAt(_target);
    }

    #region Input    
    public void OnRotate(InputValue value)
    {
        // Apply move direction
        Vector2 dir = value.Get<Vector2>().normalized;
        float direction = dir.x;
    }
    #endregion
}
