using UnityEngine;

public class Boss1Attack : MonoBehaviour
{

    public int atkDmg = 25;
    public int atkDmg2 = 34;

    public float fireRate = 15000f;
    private float timeToFire = 0f;

    private float timer;

    public Transform Boss1Gun;
    public GameObject bulletPrefab;

    public void bossAtk1(){

        //if(timeToFire <= 0f){
            //Debug.Log("Boss Firing");
            Instantiate(bulletPrefab, Boss1Gun.position, Boss1Gun.rotation);
            //timeToFire = fireRate;
        //}else{
        //    timeToFire -= Time.deltaTime;

        //}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5){

            timer = 0;
            bossAtk1();
        }
    }
}
