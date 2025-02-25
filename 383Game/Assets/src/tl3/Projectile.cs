using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float damage = 10f; // Damage dealt by the projectile
    public float lifetime = 3f; // How long the projectile exists before disappearing
    public float pushbackForce = 5f; // Force applied to ground enemies on collision
    public float droneKnockbackForce = 2f; // Force applied to floating drones

    private void Start()
    {
        // Destroy the projectile after a certain time to avoid clutter
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hit a GroundEnemy or a TrackingEnemies
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Projectile hit an enemy!");

            // Send damage to the enemy's script
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);

            // Apply appropriate force based on enemy type
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                if (collision.gameObject.name.Contains("GroundEnemy"))
                {
                    // Apply pushback to the ground enemy
                    rb.AddForce(pushDirection * pushbackForce, ForceMode2D.Impulse);
                }
                else if (collision.gameObject.name.Contains("TrackingEnemies"))
                {
                    // Apply knockback to the floating drone
                    rb.AddForce(pushDirection * droneKnockbackForce, ForceMode2D.Impulse);
                }
            }

            // Destroy the projectile upon impact with the enemy
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Projectile collided with " + collision.gameObject.name);
        }
    }
}
