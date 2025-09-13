using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public BoxCollider2D _playerBoxCollider;
    public float ballSpeed = 5f;
    public float maxBounceAngle = 75f;
    public GameObject respawnPoint;

    private Vector2 _colliderSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        _rb.linearVelocity = new Vector2(-1, 0).normalized * ballSpeed;
    }

    // Allow the ball to bounce off player and walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the ball connects with the player (and enemy later)
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

                // Update the ball's linear velocity
                _rb.linearVelocity = newVelocity.normalized * ballSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyGoal"))
        {
            // Respawn the ball back to the starting point
            Respawn();

            // TODO: Handle player scoring

        }
        else if (collision.CompareTag("PlayerGoal"))
        {
            Debug.Log("Hit the player goal!");

            // Respawn the ball back to the starting point
            Respawn();

            // TODO: Handle enemy scoring
        }
    }

    void Respawn()
    {
        // Set the position of the ball back to the respawn point
        _rb.transform.position = respawnPoint.transform.position;

        // Create a random X and Y direction for the ball
        int randomDirectionX = (Random.Range(0, 2) == 0) ? 1 : -1;
        int randomDirectionY = (Random.Range(0, 2) == 0) ? 1 : -1;

        // Reset the ball's velocity
        _rb.linearVelocity = new Vector2(randomDirectionX, randomDirectionY).normalized * ballSpeed;
    }
}
