using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerMovement : MonoBehaviour
{
    Animator _animator;
    ScPlayerStats _stats;
    Rigidbody2D _rb;
    private float _speed;
    private Vector2 _moveInput;
    [SerializeField] private float _deadZone = 0.1f; // for joystick drift
    [SerializeField] private float _runningSpeed = 5f;
    private bool _isFacingDown = true;
    private bool _isFacingUp = false;
    private bool _isFacingRight = false;
    private bool _isFacingLeft = false;
    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _canMove = true;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _stats = GetComponent<ScPlayerStats>();
        _rb = GetComponent<Rigidbody2D>();

        _speed = _stats.GetSpeed();
    }
    void Update()
    {
        if (!_canMove) return;

        ForAnimator();
        Move();
    }
    public void CantMove()
    {
        _canMove = false; // pour cancel les autres actions
        _animator.SetBool("IsMoving",false);
        _animator.SetBool("IsRunning",false);
    }
    public void CanMove()
    {
        _canMove = true; 
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            Vector2 rawInput = context.ReadValue<Vector2>();

            _moveInput = (rawInput.magnitude > _deadZone) ? rawInput : Vector2.zero;

        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isRunning = true;
        }
        else if (context.canceled)
        {
            _isRunning = false;
        }
    }

    void Move()
    {
        if (!_isRunning && _canMove)
        _rb.linearVelocity = _moveInput.normalized * _speed;
        else if (_canMove)
        _rb.linearVelocity = _moveInput.normalized * _runningSpeed;
    }

    void ForAnimator()
    {
        if (Time.timeScale == 0) return;

        if (_moveInput != Vector2.zero)
        {
            _isMoving = true;

            // Déterminer la direction principale
            if (Mathf.Abs(_moveInput.y) > Mathf.Abs(_moveInput.x))
            {
                if (_moveInput.y > 0)
                {
                    _isFacingUp = true;
                    _isFacingDown = false;
                    _isFacingRight = false;
                    _isFacingLeft = false;
                }
                else
                {
                    _isFacingUp = false;
                    _isFacingDown = true;
                    _isFacingRight = false;
                    _isFacingLeft = false;
                }
            }
            else
            {
                if (_moveInput.x > 0)
                {
                    _isFacingUp = false;
                    _isFacingDown = false;
                    _isFacingRight = true;
                    _isFacingLeft = false;
                }
                else
                {
                    _isFacingUp = false;
                    _isFacingDown = false;
                    _isFacingRight = false;
                    _isFacingLeft = true;
                }
            }
        }
        else
        {
            _isMoving = false;
        }

        _animator.SetBool("FaceDown",_isFacingDown);
        _animator.SetBool("FaceUp",_isFacingUp);
        _animator.SetBool("FaceRight",_isFacingRight);
        _animator.SetBool("FaceLeft",_isFacingLeft);

        _animator.SetBool("IsMoving",_isMoving);
        _animator.SetBool("IsRunning",_isRunning);


    }
}
