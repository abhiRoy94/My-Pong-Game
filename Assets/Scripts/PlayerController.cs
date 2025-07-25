using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public InputSystem_Actions _playerControls;
    public float moveSpeed = 5.0f;

    private InputAction _move;
    private Vector2 _moveDirection = Vector2.zero;


    public void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();
    }

    public void OnDisable()
    {
        _move.Disable();
    }

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }


    // Update is called once per frame
    void Update()
    {
        _moveDirection = _move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }
}
