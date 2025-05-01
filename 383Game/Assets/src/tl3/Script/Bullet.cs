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
        if (collision.CompareTag("Player")) return;

        // ✅ Check Enemy
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyStats>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        // ✅ Check Boss (regardless of tag)
        var boss = collision.GetComponent<Boss1Stats>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        // Destroy bullet
        Destroy(gameObject);
    }

}
