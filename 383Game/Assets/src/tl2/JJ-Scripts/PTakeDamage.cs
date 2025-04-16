using UnityEngine;

public class EnemyCauseDamage : MonoBehaviour
{
    public PlayerStats playerHealth; //reference player's stats
    public float damage = 5f;
    public Move _movement; //ref to movement for knockback


    //called when an enemy or enemy projectile collides with another collider (player)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") //see if its obj with Player tag
        {
            _movement._KBCounter = _movement._KBTotalTime; //start knockback
            //determine direction of knockback based on player/enemy positions
            if (collision.transform.position.x <= transform.position.x)
            {
                _movement._HitFromRight = true;
            }
            else
            {
                _movement._HitFromRight = false;
            }
            

            bool bcMode = PlayerPrefs.GetInt("BCMode",0) == 1;
            if(!bcMode) //only apply damage if bc mode is off
            {
                AudioManager.playSound(SoundType.HURT);
                playerHealth.TakeDamage(damage); 
                //the damage sent to player's health - can be defined above or in inspector for that enemy
            }
        }
    }
}
