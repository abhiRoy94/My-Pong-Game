using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public BoxCollider2D _playerBoxCollider;
    public float ballSpeed = 2.5f;
    public float maxBounceAngle = 75f;

    private Vector2 _colliderSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        _rb.linearVelocity = new Vector2(-1, -1).normalized * ballSpeed;
    }

    // Allow the ball to bounce off player and walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit the player!");
            _playerBoxCollider = collision.collider as BoxCollider2D;  
            
            if (_playerBoxCollider != null )
            {
                // Get the player information
                float playerHeight = _playerBoxCollider.size.y;
                Vector2 playerMidpoint = _playerBoxCollider.bounds.center;

                // Determine the collision point, and collision velocity
                Vector2 collisionPoint = collision.GetContact(0).point;
                //Vector2 collisionVelocity = collision.GetContact(0).relativeVelocity;

                // Calculate the normalized height difference between the collision and the player midpoint
                float heightDifference = playerMidpoint.y - collisionPoint.y;
                float normalizedDistance = heightDifference / (playerHeight / 2f);

                // Calculate the ball bounce angle
                float bounceAngle = normalizedDistance * (maxBounceAngle * Mathf.Deg2Rad);
                
                // Get the current horizontal direction of the ball
                float currentDirectionX = Mathf.Sign(_rb.linearVelocity.x);
                float currentDirectionY = Mathf.Sign(_rb.linearVelocity.y);
                
                // Calculate the new ball speeds in the X and Y direction

                Vector2 newVelocity = new Vector2(-currentDirectionX * Mathf.Cos(bounceAngle), currentDirectionY * Mathf.Sin(bounceAngle));
                //float ballVx = collisionVelocity.x * Mathf.Cos(bounceAngle);
                //float ballVy = collisionVelocity.y * -Mathf.Sin(bounceAngle);

                Debug.Log("BallVx: " + newVelocity.x + ", BallVy: " + newVelocity.y);


                _rb.linearVelocity = newVelocity.normalized * ballSpeed;
            }
        }
    }
}
