using UnityEngine;

public class EnemyStatsPrivatePattern : MonoBehaviour
{
    // Private class data holder
    private class EnemyData
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public int BaseAttackDmg { get; set; }
    }

    private EnemyData data = new EnemyData();

    // Expose only what’s necessary
    public float Health => data.Health;
    public float MaxHealth => data.MaxHealth;
    public int BaseAttackDmg => data.BaseAttackDmg;

    public float initialMaxHealth = 100f;
    public int initialBaseAttackDmg = 10;

    void Start()
    {
        data.MaxHealth = initialMaxHealth;
        data.Health = data.MaxHealth;
        data.BaseAttackDmg = initialBaseAttackDmg;
    }

    void Update()
    {
        // update health bar or UI here
    }

    public void TakeDamage(float damage)
    {
        data.Health -= damage;
        if (data.Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
