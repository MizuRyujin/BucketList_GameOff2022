using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private float _rotationFactor = 5f;

    private bool _isGrounded;
    private bool _shouldJump;
    private Rigidbody _rb;
    private Vector3 _playerInput;
    private Vector2 _mouseInput;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>>
    /// Update is called once per frame
    /// </summary>>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.LogWarning("Quitting on ESCAPE");
            Application.Quit();
        }
        GetPlayerInput();
        CheckForJump();
        RotateYAxis();
    }

    private void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shouldJump = true;
        }
    }

    private void GetPlayerInput()
    {
        _playerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        PlayerMove();
        IsGrounded();
        PlayerJump();
    }


    private void IsGrounded()
    {
        Collider[] colliders = new Collider[1];
        int hits = Physics.OverlapSphereNonAlloc(transform.position, 0.1f, colliders, LayerMask.GetMask("Ground"));
        if (hits > 0)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private void PlayerMove()
    {
        Vector3 curVel = _rb.velocity;
        Vector3 moveDir = _rb.transform.TransformDirection(_playerInput) * _acceleration * Time.fixedDeltaTime;

        if (!_isGrounded)
        {
            moveDir *= 0.5f;
        }
        curVel += moveDir;

        _rb.velocity = new Vector3(curVel.x, _rb.velocity.y, curVel.z);
    }

    private void RotateYAxis()
    {

        Quaternion camRot = Quaternion.Euler(0f, _mouseInput.x * _rotationFactor * Time.fixedDeltaTime, 0f);
        _rb.MoveRotation(_rb.rotation * camRot);
    }

    private void PlayerJump()
    {
        if (_isGrounded && _shouldJump)
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
        _shouldJump = false;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
