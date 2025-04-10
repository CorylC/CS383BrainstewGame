using UnityEngine;

public class BasicBulletPath : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 15f; // Fixed speed for horizontal movement
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Move the projectile in a fixed horizontal direction (right or left)
        rb.linearVelocity = new Vector2(-speed, 0); // Moves right. Use -speed for left.
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")){
            Destroy(gameObject);
        }
    }
}
