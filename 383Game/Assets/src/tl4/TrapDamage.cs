using UnityEngine;
using System.Collections;

/*
This script is attached to the trap game object.
When the player enters the trap, it applies damage over time and slows down the player.
When the player exits the trap, it stops applying damage and removes the slowing effect.

Trap Damage script uses the observer pattern because the built-in functions of Unity such as OnTriggerEnter2D and OnTriggerExit2D
are Unity's way of notifying the script when a player enters or exits the trap. This follows the observer pattern because there is
a dependency on the player object, and the trap is observing the player's state (whether they are inside or outside the trap).

The script also uses the decorator pattern because by modifying the player's state (applying damage over time and slowing down the player),
it dynamically adds behavior to the player while inside the trap zone. The intent of the decorator pattern is to add functionality
to an object without modifying its structure. 

My patterns are work great together because the observer pattern allows the trap to react to the player's state changes (entering and exiting the trap),
while the decorator pattern allows the trap to modify the player's behavior (applying damage and slowing down) without changing the player's class or structure. 

State pattern is best when an object's behavior changes based on its internal state like idle, activated, cooldown, disabled. Using the state
pattern would mean encapsulating each of those behaviors in its own class.

Command pattern is best when you want to encapsulate a request or action as an object, ideal for decouploing the sender and receiver. This would be
the best approach if I could do this project again. The trap would trigger a command to apply damage and/or slow down the player, the player's
system would execute that command.

I could have used the State or Command patterns, but the Observer pattern is more suitable for this case because the trap listens (observes) for changes in 
the player's presence—whether the player enters or exits the trap’s area—and responds accordingly. The trap does not represent a state of the 
player (this would be the State pattern), nor is it actively sending commands (like the Command pattern); instead, it reacts passively based 
on the player's behavior, making the Observer pattern the most appropriate choice.

I could use state pattern if I wanted to create different states for the trap itself (e.g., active, inactive, triggered) and have the trap 
change its behavior based on its state.
The command pattern would be more suitable if I wanted to create a system where the trap could issue commands to the player (e.g., "take damage",
 "slow down") and have the player respond to those commands.
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
            PlayerStats playerStats = other.GetComponent<PlayerStats>(); // Get the PlayerStats component from the player
            targetRB = other.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the player
            
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