using UnityEngine;
using System.Collections;

/*
This script is attached to the trap game object.
When the player enters the trap, it applies damage over time and slows down the player.
When the player exits the trap, it stops applying damage and removes the slowing effect.
*/

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage; // The amount of damage the trap deals to the player
    [SerializeField] private float damageInterval; // The interval between damage ticks
    [SerializeField] private float slowMultiplier = 0.5f; // Slow down the player by 50%

    private Coroutine damageCoroutine; // Coroutine for applying damage over time
    private Coroutine slowCoroutine; // Coroutine for applying slowing effect

    private Rigidbody2D targetRB; // The Rigidbody2D of the player that enters the trap
    private bool playerInTrap = false; // Flag to check if the player is in the trap

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            targetRB = other.GetComponent<Rigidbody2D>();
            //Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if(playerStats != null && targetRB != null) // Check if the player has PlayerStats component
            {
                playerInTrap = true; // Set the flag to true when the player enters the trap


                if(damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(playerStats)); // Start applying damage over time
                }

                if(slowCoroutine == null)
                {
                    slowCoroutine = StartCoroutine(ApplySlowing()); // Start applying slowing effect
                }

                Debug.Log("Player entered trap and will be slowed down.");
            }
            else
            {
                Debug.LogError("PlayerStats component not found on the player.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrap = false; // Set the flag to false when the player exits the trap


            if(damageCoroutine != null) // Check if the coroutine is running
            {
                StopCoroutine(damageCoroutine); // Stop applying damage over time
                damageCoroutine = null; // Reset the coroutine reference
            }

            if(slowCoroutine != null) // Check if the coroutine is running
            {
                StopCoroutine(slowCoroutine); // Stop applying slowing effect
                slowCoroutine = null; // Reset the coroutine reference
            }

            targetRB = null; // Reset the targetRB when the player exits the trap

            Debug.Log("Player exited trap and will no longer be slowed down.");
        }
    }


    // Coroutine to apply damage over time to the player
    // This coroutine will run indefinitely until stopped
    private IEnumerator ApplyDamageOverTime(PlayerStats playerStats)
    {
        while (true) 
        {
            playerStats.TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage from trap.");
            yield return new WaitForSeconds(damageInterval);
        }
    }


    // Coroutine to apply slowing effect to the player
    // This coroutine will run indefinitely until stopped
    private IEnumerator ApplySlowing()
    {
        while(playerInTrap && targetRB != null)
        {
            Vector2 velocity = targetRB.linearVelocity;
            targetRB.linearVelocity = new Vector2(velocity.x * slowMultiplier, velocity.y);
            Debug.Log("Player is slowed down by the trap.");
            yield return new WaitForFixedUpdate();
        }
    }
}