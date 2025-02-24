using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public int baseAttackDmg = 10;
    //could add defense or speed 
    public Image healthBar;

    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }


    void Update()
    {
        UpdateHealthBar();

        if(health <= 0)
        {
            //player die animation
            Destroy(gameObject);
            //trigger game over screen
        }
    }

    void UpdateHealthBar()
    {
        if(healthBar != null)
        {
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth,0,1);
        }
    }

    //any time another script calls this function, it can send in however much dmg it sends to player
    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();

        if(health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount,maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        //player die logic and animation
        gameObject.SetActive(false);
        TriggerGameOver();
    }

    void TriggerGameOver()
    {
        //load the game over screen when player dies
        SceneManager.LoadScene(4);
    }

    public float GetBaseAttackDmg()
    {
        return baseAttackDmg;
    }
}
