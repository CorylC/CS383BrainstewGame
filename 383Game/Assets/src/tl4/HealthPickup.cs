using UnityEngine;


/*
This script is attached to a health pickup object in the game.
When the player collides with the pickup, it heals the player by a specified amount.
*/

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20; // Amount of health to restore

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerHealth = collision.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                Destroy(gameObject); // Remove the power-up from the scene
            }
        }
    }
}

