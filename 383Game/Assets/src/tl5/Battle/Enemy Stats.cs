using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public int baseAttackDmg = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //player die logic and animation
        gameObject.SetActive(false);
    }
}
