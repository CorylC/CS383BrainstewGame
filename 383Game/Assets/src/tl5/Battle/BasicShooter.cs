using System.Threading;
using UnityEngine;

// Shoots bullets without worrying about distance from Player

public class BasicShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private GameObject player;
    private CannonShootDynamicBindingBC cannon; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cannon = new CannonShootDynamicBinding();  // instantiate it
        cannon.setSpeed(15);
        Debug.Log("Cannon speed is: " + cannon.getSpeed());
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);
        timer += Time.deltaTime;
        if(timer > 4){
        timer = 0;
        shoot();
        
        }

        
    }

    void shoot()
    {
        AudioManager.playSound(SoundType.CANNONSHOOT);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Debug.Log("Bullet shot at speed: " + cannon.getSpeed());
    }

    
}



