using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageInterval;
    public float DamageInterval => damageInterval;

    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if(playerStats != null)
            {
                if(damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(playerStats));
                }
                Debug.Log("Player entered trap and will take damage over time.");
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
            if(damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                Debug.Log("Player exited trap and will no longer take damage.");
            }
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
}