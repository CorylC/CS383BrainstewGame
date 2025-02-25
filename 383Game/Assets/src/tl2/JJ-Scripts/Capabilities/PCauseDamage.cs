using UnityEngine;

public class PCombat : MonoBehaviour
{
    public EnemyStats enemyHealth;
    public float damage = 5f;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
