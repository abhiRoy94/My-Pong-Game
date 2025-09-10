using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public BoxCollider2D _playerBoxCollider;
    public float ballSpeed = 2.5f;

    private Vector2 _colliderSize;

    // Temporarily set the ball's direction to go left
    private Vector2 _direction = new Vector2(-1, -1);

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

            _playerBoxCollider = collision.collider as BoxCollider2D;  
            if ( _playerBoxCollider != null )
            {
                // Get the midpoint of the player 
                Vector2 _playerMidpoint = _playerBoxCollider.bounds.center;

                // Determine the collision point between the ball and the player
                Vector2 _collisionPoint = collision.GetContact(0).point;

                // Calculate the height difference between the collision and the player midpoint
                float _heightDifference = _playerMidpoint.y - _collisionPoint.y;
                Debug.Log("Height Difference between midpoint and collision: " + _heightDifference);
            }

            _direction = new Vector2(-_direction.x, -_direction.y);
        }
    }
}
