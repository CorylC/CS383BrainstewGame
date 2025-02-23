using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;

    void Start()
    {
        maxHealth = health;
    }


    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health/maxHealth,0,1);

        if(health <= 0)
        {
            //player die animation
            Destroy(gameObject);
            //trigger game over screen
        }
    }
}
