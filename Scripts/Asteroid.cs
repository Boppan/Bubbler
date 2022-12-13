using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int asteroidSpeedX = 7;
    private int asteroidSpeedY = 3;
    private Rigidbody2D asteroidRB2D;
    private Vector3 lastVelocity;

    public GameObject explosion;

    private PlayerState playerState;


    // Start is called before the first frame update
    void Start()
    {
        asteroidRB2D = GetComponent<Rigidbody2D>();
        asteroidRB2D.velocity = new Vector2(asteroidSpeedX, asteroidSpeedY);
        playerState = FindObjectOfType<PlayerState>();
        
    }

    void Update()
    {
        lastVelocity = asteroidRB2D.velocity;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            playerState.TakeDamage(5);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            asteroidRB2D.velocity = direction * Mathf.Max(0f, speed);
        }
        
    }
}   