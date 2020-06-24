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

    [SerializeField]
    [Tooltip("How fast the camera rotates in angles")]
    private float _cameraSensitivity = 1;

    private float _height = 0;
    private float _radius = 0;
    [SerializeField]
    private float _angle = 0;
    private float _rotateDirection = 0;

    private void Start()
    {
        _height = _offset.y;
        _radius = Mathf.Sqrt(Mathf.Pow(_offset.z, 2) + Mathf.Pow(_offset.x, 2));
        Vector2 _direction = new Vector2(_offset.x, _offset.z).normalized;
        if (_direction.y >= 0)
            _angle = Mathf.Acos(_direction.x);
        else
            _angle = -Mathf.Acos(_direction.x);
    }

    void Update()
    {
        // Smooth follow the position
        Vector3 desiredPos = GetDesiredPosition();
        transform.position = Vector3.Lerp(transform.position, desiredPos, _smoothStep * Time.deltaTime);

        if (!Application.isPlaying)
            transform.position = desiredPos;

        transform.LookAt(_target);
    }

    private Vector3 GetDesiredPosition()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            _height = _offset.y;
            _radius = Mathf.Sqrt(Mathf.Pow(_offset.z, 2) + Mathf.Pow(_offset.x, 2));
            Vector2 _direction = new Vector2(_offset.x, _offset.z).normalized;
            if (_direction.y >= 0)
                _angle = Mathf.Acos(_direction.x);
            else
                _angle = -Mathf.Acos(_direction.x);
        }

        Vector3 offset = new Vector3(0, _height, 0);
        offset.x = _radius * Mathf.Cos(_angle);
        offset.z = _radius * Mathf.Sin(_angle);
        return _target.position + offset;
    }

    #region Input    
    public void OnRotate(InputValue value)
    {
        // Apply move direction
        Vector2 dir = value.Get<Vector2>().normalized;
        _rotateDirection = dir.x;
        _angle += Mathf.PI * .5f * dir.x;
    }
    #endregion
}
