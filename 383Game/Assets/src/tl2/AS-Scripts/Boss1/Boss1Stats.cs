using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss1Stats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int baseAttackDmg = 10;
    public int point = 0;

    public PointManager pointManager;

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
        //send points to point manager
        pointManager.AddPoints(point);
        gameObject.SetActive(false);
        SceneManager.LoadScene(8);
    }
}

//Taken from TL5 Enemy Stats.cs and (very slightly) modified to better suit the boss