using UnityEngine;

public class EnemyCauseDamage : MonoBehaviour
{
    public PlayerStats playerHealth;
    public float damage = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
