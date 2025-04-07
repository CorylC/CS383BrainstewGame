using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageInterval;
    [SerializeField] private float slowMultiplier = 0.5f; // Slow down the player by 50%

    private Coroutine damageCoroutine;
    private Coroutine slowCoroutine;

    private Rigidbody2D targetRB;
    private bool playerInTrap = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            targetRB = collision.GetComponent<Rigidbody2D>();
            //Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if(playerStats != null && targetRB != null)
            {
                playerInTrap = true;


                if(damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(playerStats));
                }

                if(slowCoroutine == null)
                {
                    slowCoroutine = StartCoroutine(ApplySlowing());
                }

                Debug.Log("Player entered trap and will be slowed down.");
            }
            else
            {
                Debug.LogError("PlayerStats component not found on the player.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrap = false;


            if(damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

            if(slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
                slowCoroutine = null;
            }

            targetRB = null; // Reset the targetRB when the player exits the trap

            Debug.Log("Player exited trap and will no longer be slowed down.");
        }
    }

    private IEnumerator ApplyDamageOverTime(PlayerStats playerStats)
    {
        while (true)
        {
            playerStats.TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage from trap.");
            yield return new WaitForSeconds(damageInterval);
        }
    }

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