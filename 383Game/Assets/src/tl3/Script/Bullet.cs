using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet movement speed
    public float speed = 15f;

    // How much damage this bullet deals to enemies
    public float damage = 10f;

    // Bullet lifespan in seconds (auto-destroy after this time)
    public float lifetime = 2f;

    // Rigidbody2D reference for controlling bullet physics
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component attached to this bullet
        rb = GetComponent<Rigidbody2D>();

        // Set the bullet's velocity in the direction it’s facing
        rb.linearVelocity = transform.right * speed;

        // Automatically destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collisions with the player
        if (collision.CompareTag("Player")) return;

        // If the bullet hits an enemy, apply damage
        if (collision.CompareTag("Enemy"))
        {
            // Try to get the EnemyStats script
            var enemy = collision.GetComponent<EnemyStats>();

            // Try to get the Boss1Stats script (for larger enemies/bosses)
            Boss1Stats boss = collision.GetComponent<Boss1Stats>();

            // If the enemy has health logic, apply damage
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        // Destroy the bullet on any collision (except Player)
        Destroy(gameObject);
    }
}
