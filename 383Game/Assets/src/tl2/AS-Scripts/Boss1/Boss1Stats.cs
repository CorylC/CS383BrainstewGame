using UnityEngine;

public class Boss1Stats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int baseAttackDmg = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        //update healthbar here
    }

    void Update()
    {
        //enemy HUD updates here
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
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

//Taken from TL5 Enemy Stats.cs and (will be) modified to better suit the boss