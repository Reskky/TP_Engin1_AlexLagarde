using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public int PlayerPoints { get; private set; }

    [Header("Utility")]
    private LayerMask _jumpableLayerMask;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpVelocity = 7.5f;
    [SerializeField] private float _coyoteJumpTimer = 0.150f;
    [Range(0, 1)] [SerializeField] private float _midAirSpeedMultiplier = 1.0f;
    
    [Header("Debug")]
    [SerializeField] private float _horizontalMovement;

    [SerializeField] private bool _isGrounded;

    private Rigidbody _rb;
    private Collider _collider;

    private bool _canCoyoteJump;
    private bool _canJump = true;
    
    private void Awake()
    {
        GetComponents();
    }
    
    private void GetComponents()
    {
        if (!_rb)
            _rb = GetComponent<Rigidbody>();
        if (!_collider)
            _collider = GetComponent<Collider>();
    }
    
    private void Start()
    {
        AddListeners();
        SetLayerMask();
    }

    private void SetLayerMask()
    {
        _jumpableLayerMask = LayerMask.GetMask("Ground", "Crate", "Platform");
    }

    private void AddListeners()
    {
        InputHandler.Instance.OnMove += SetMoveInput;
        InputHandler.Instance.OnJump += Jump;
    }
    
    private void FixedUpdate()
    {
        Move();
        IsPlayerGrounded();
    }

    private void Move()
    {
        _rb.linearVelocity = _isGrounded ? new Vector3(_horizontalMovement * _moveSpeed, _rb.linearVelocity.y, 0):
            new Vector3(_horizontalMovement * (_moveSpeed * _midAirSpeedMultiplier), _rb.linearVelocity.y, 0);
    }

    private void SetMoveInput(float value)
    {
        _horizontalMovement = value;
    }
    
    private void Jump()
    {
        if (!_isGrounded && _canJump)
        {
            StartCoroutine(CoyoteJump());
        }
        
        if (_isGrounded || _canCoyoteJump)
        {
            Vector3 v = _rb.linearVelocity;
            v.y = _jumpVelocity;
            _rb.linearVelocity = v;

            _canJump = false;
        }
    }

    private IEnumerator CoyoteJump()
    {
        _canCoyoteJump = true;

        yield return new WaitForSeconds(_coyoteJumpTimer);

        _canCoyoteJump = false;
    }
    
    private void IsPlayerGrounded()
    {
        Bounds b = _collider.bounds;

        if (Physics.Raycast(b.min, Vector3.down, 0.1f, _jumpableLayerMask))
        {
            Debug.Log("Grounded");
            _isGrounded = true;
            _canJump = true;
            return;
        }
        _isGrounded = false;
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("In collision with coin");
            PlayerPoints++;
            other.gameObject.SetActive(false);
        }
    }
}
 