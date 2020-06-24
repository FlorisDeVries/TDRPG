using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


/// <summary>
/// Class that handles all movement logic for the player
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerHealth _playerHealth;

    [SerializeField]
    [Tooltip("How fast the character moves")]
    private float _speed = 5.0f;

    [SerializeField]
    [Tooltip("How high we jump")]
    private float _jumpHeight = 2.0f;
    private bool _jumping = false;
    private float _jumpTimer = 0f;
    private float _groundedTimer = 0f;


    [SerializeField]
    [Tooltip("How fast we fall")]
    private float _gravity = -9.81f;

    [SerializeField]
    [Tooltip("How much drag should be applied to velocity")]
    private Vector3 _drag = Vector3.one;

    private Vector3 _velocity = Vector3.zero;

    [SerializeField]
    private Vector3 _moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerHealth = GetComponent<PlayerHealth>();
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
        if (!_jumping)
            _jumpTimer = .2f;
    }

    public void Update()
    {
        if (_playerHealth.IsDead || GameStateManager.Instance.GameState == GameState.GameOver)
        {
            AddGravity();
            _characterController.Move(_velocity * Time.deltaTime);
            return;
        }

        // Check jumping
        if ((_jumping || _jumpTimer > 0f) && _groundedTimer > 0f)
        {
            _velocity.y += _jumpHeight;

            _groundedTimer = 0f;
            _jumpTimer = 0f;
        }

        // Jump timer, remembers the jump button press for a bit. This makes the jump feel more reactive
        if (_jumpTimer > 0f)
            _jumpTimer -= Time.deltaTime;
        if (_groundedTimer > 0f)
            _groundedTimer -= Time.deltaTime;

        // Check gravity
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
            _groundedTimer = .2f;
        }

        // Move the character
        // Move direction is local, not taking into account the direction of the camera
        _characterController.Move(MakeMovementRelative() * Time.deltaTime * _speed);

        AddGravity();
        _characterController.Move(_velocity * Time.deltaTime);

        // Look at the mouse
        Vector3 mousePos = Utils.GetPlaneIntersection(transform.position.y);
        mousePos.y = transform.position.y;
        transform.LookAt(mousePos);
    }

    private void AddGravity()
    {
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
    }

    private Vector3 MakeMovementRelative()
    {
        // Get forward
        Vector3 forward = MainCamera.Instance.transform.forward;
        forward.y = 0;
        forward.Normalize();

        // Get right
        Vector3 right = MainCamera.Instance.transform.right;
        right.y = 0;
        right.Normalize();

        return forward * _moveDirection.z + right * _moveDirection.x;
    }
}
