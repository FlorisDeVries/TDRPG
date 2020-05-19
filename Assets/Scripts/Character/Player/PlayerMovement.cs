﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IDamageable
{
    private CharacterController _characterController;

    [SerializeField]
    [Tooltip("How fast the character moves")]
    private float _speed = 5.0f;

    [SerializeField]
    [Tooltip("How high we jump")]
    private float _jumpHeight = 2.0f;

    [SerializeField]
    [Tooltip("How fast we fall")]
    private float _gravity = -9.81f;
    private bool _jumping = false;
    private bool _firing = false;

    [SerializeField]
    [Tooltip("How high we jump")]
    private float _dashDistance = 5.0f;

    [SerializeField]
    [Tooltip("How much drag should be applied to velocity")]
    private Vector3 _drag = Vector3.one;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _moveDirection = Vector3.zero;

    [Header("Projectiles")]

    [SerializeField]
    [Tooltip("The position for the projectiles to be fired")]
    private Transform _firePoint = default;

    [SerializeField]
    [Tooltip("The standard prefab for the shard projectile")]
    private GameObject _shardPrefab = default;

    private float _fireCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void OnFire()
    {
        _firing = !_firing;
    }

    public void OnMove(InputValue value)
    {
        // Apply move direction
        Vector2 dir = value.Get<Vector2>().normalized;
        _moveDirection = new Vector3(dir.x, 0, dir.y);
    }

    public void OnJump()
    {
        // Jump if we are grounded
        _jumping = !_jumping;
    }

    public void OnDash()
    {
        // Dash where the player is looking
        _velocity += Vector3.Scale(transform.forward, _dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _drag.z + 1)) / -Time.deltaTime)));
    }

    public void Update()
    {
        // Fire rate
        _fireCooldown -= Time.deltaTime;

        // Firing
        if (_firing && _firePoint && _fireCooldown < 0f)
        {
            ProjectileMove projectile = Instantiate(_shardPrefab, _firePoint.transform.position, _firePoint.transform.rotation).GetComponent<ProjectileMove>();
            _fireCooldown = 1 / projectile.FireRate;
            CameraShake.Instance.Shake(Random.Range(.01f, .05f));
        }

        // Check gravity
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;

            if (_jumping)
            {
                _velocity.y += _jumpHeight;
            }
        }

        // Move the character
        _characterController.Move(_moveDirection * Time.deltaTime * _speed);

        // Add gravity
        if (_velocity.y > 0)
        {
            if (_jumping)
                _velocity.y += _gravity * Time.deltaTime;
            else
                _velocity.y += 2f * _gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y += 3f * _gravity * Time.deltaTime;
        }

        _characterController.Move(_velocity * Time.deltaTime);

        // Look at the mouse
        Vector3 mousePos = Utils.GetPlaneIntersection(transform.position.y);
        mousePos.y = transform.position.y;
        transform.LookAt(mousePos);
    }

    public void Hit(float damage)
    {
        Debug.LogError("Implement this behaviour please");
    }
}
