using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    public EnemyStats enemyHealth;
    public float damage = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyHealth.TakeDamage(damage);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
