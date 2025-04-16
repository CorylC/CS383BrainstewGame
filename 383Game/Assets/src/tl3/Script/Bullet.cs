using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 10f;
    public float lifetime = 2f; // Destroy bullet after X seconds

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Move bullet forward based on its facing direction
        rb.linearVelocity = transform.right * speed;

        // Auto-destroy to avoid memory mess
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        // Damage if it hits enemy
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyStats>();
            Boss1Stats boss = collision.GetComponent<Boss1Stats>();  // Get Boss1Stats script
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        // Destroy the bullet on any hit (except Player)
        Destroy(gameObject);
    }
}
