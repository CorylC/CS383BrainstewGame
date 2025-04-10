using UnityEngine;

public class BulletDamagePlayer : MonoBehaviour
{
    public PlayerStats playerHealth;
    public float damage = 5f;
    public Move _movement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _movement._KBCounter = _movement._KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                _movement._HitFromRight = true;
            }
            else
            {
                _movement._HitFromRight = false;
            }
            

            bool bcMode = PlayerPrefs.GetInt("BCMode",0) == 1;
            if(!bcMode)
            {
                AudioManager.playSound(SoundType.HURT);
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
