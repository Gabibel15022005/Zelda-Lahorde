using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerMovement : MonoBehaviour
{
    ScPlayerStats _stats;
    Rigidbody2D _rb;
    private float _speed;
    private Vector2 _moveInput;
    [SerializeField] private float _deadZone = 0.1f; // for joystick drift
    void Awake()
    {
        _stats = GetComponent<ScPlayerStats>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _speed = _stats.GetSpeed();
    }
    void Update()
    {
        Move();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            Vector2 rawInput = context.ReadValue<Vector2>();

            _moveInput = (rawInput.magnitude > _deadZone) ? rawInput : Vector2.zero;
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    void Move()
    {
        _rb.linearVelocity = _moveInput.normalized * _speed;
    }
}
