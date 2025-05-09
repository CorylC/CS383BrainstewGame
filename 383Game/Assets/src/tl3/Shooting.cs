using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    void Start()
    {
        mainCam = Camera.main; // More efficient
    }

    void Update()
    {
        if(PauseManager.isPaused) //prevent shooting during pause
        {
            return;
            //exit early if the game is paused (this way player cannot shoot when paused)
        }
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = (mousePos - transform.position);

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, rotZ); // Fixes rotation distortion

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            AudioManager.playSound(SoundType.SHOOT);
        }
    }
}
