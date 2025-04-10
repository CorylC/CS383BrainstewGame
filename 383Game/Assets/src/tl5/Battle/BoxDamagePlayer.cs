using UnityEngine;

public class BoxDamagePlayer : MonoBehaviour
{
    public float damage = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Dynamically get the components from the collided player
            PlayerStats playerHealth = collision.GetComponent<PlayerStats>();
            Move movement = collision.GetComponent<Move>();

            if (movement != null)
            {
                movement._KBCounter = movement._KBTotalTime;
                movement._HitFromRight = (collision.transform.position.x <= transform.position.x);
            }

            bool bcMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
            if (!bcMode && playerHealth != null)
            {
                AudioManager.playSound(SoundType.HURT);
                playerHealth.TakeDamage(damage);
            }
        }
    }

}
