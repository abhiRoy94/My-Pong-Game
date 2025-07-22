using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float ballSpeed = 5.0f;

    // Temporarily set the ball's direction to go left
    private Vector2 _direction = new Vector2(-1, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_direction.x * ballSpeed, _direction.y * ballSpeed);
    }

    // Allow the ball to bounce off player and walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit the player!");
            _direction = new Vector2(-_direction.x, -_direction.y);
        }
    }
}
