using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private float _jumpTime = 2f;
    [SerializeField] private float _rotationFactor = 5f;

    private bool _isGrounded;
    private Rigidbody _rb;
    private Vector3 _playerInput;
    private Vector2 _mouseInput;
    private float _jumpTimer;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpTimer = _jumpTime;
    }

    /// <summary>>
    /// Update is called once per frame
    /// </summary>>
    private void Update()
    {
        _playerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        RotateCamera();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        IsGrounded();
        PlayerMove();
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
        Vector3 moveDir = transform.TransformDirection(_playerInput) * _acceleration;

        var currSpeed = _rb.velocity;
        _rb.velocity = new Vector3(moveDir.x, _rb.velocity.y, moveDir.z);
        // if (_rb.velocity.sqrMagnitude > 10)
        // {
        //     _rb.velocity = currSpeed;
        // }
    }

    private void RotateCamera()
    {
        transform.Rotate(transform.up, _mouseInput.x * _rotationFactor);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(_isGrounded || _jumpTimer > 0f)
            {
                _rb.AddForce(transform.up * _jumpForce);
                _jumpTimer -= 0.1f * Time.fixedDeltaTime;
            }
        }
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
