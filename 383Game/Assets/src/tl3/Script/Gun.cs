using UnityEngine;

public enum GunMode
{
    Normal,
    Laser,
    Shotgun,
    RapidFire
}

public class Gun : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;
    public GameObject laserBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject rapidFireBulletPrefab;

    public GameObject muzzleFlashPrefab;
    public GameObject shellPrefab;
    public Transform shellEjectPoint;

    public GunMode currentMode = GunMode.Normal;

    private float rapidFireCooldown = 0.1f; // How fast rapidfire shoots
    private float lastFireTime;

    void Update()
    {
        if (currentMode == GunMode.RapidFire)
        {
            if (Input.GetMouseButton(0)) // hold to rapid fire
            {
                if (Time.time - lastFireTime >= rapidFireCooldown)
                {
                    Fire();
                    lastFireTime = Time.time;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) // click for other modes
            {
                Fire();
            }
        }
    }


    public void Fire()
    {
        GameObject prefabToUse = bulletPrefab;

        if (currentMode == GunMode.Laser && laserBulletPrefab != null)
            prefabToUse = laserBulletPrefab;
        else if (currentMode == GunMode.Shotgun && shotgunBulletPrefab != null)
            prefabToUse = shotgunBulletPrefab;
        else if (currentMode == GunMode.RapidFire && rapidFireBulletPrefab != null)
            prefabToUse = rapidFireBulletPrefab;

        if (prefabToUse == null)
        {
            Debug.LogWarning("❌ Bullet prefab is missing! Mode: " + currentMode);
            return;
        }

        Debug.Log("✅ Firing prefab: " + prefabToUse.name);
        Instantiate(prefabToUse, firePoint.position, firePoint.rotation);

        // 🔊 Play gun sound
        AudioManager.playSound(SoundType.SHOOT);


        if (muzzleFlashPrefab != null)
        {
            Debug.Log("✨ MuzzleFlash: " + muzzleFlashPrefab.name);
            Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        }

        if (shellPrefab != null && shellEjectPoint != null)
        {
            Debug.Log("💥 Shell Eject: " + shellPrefab.name);
            Instantiate(shellPrefab, shellEjectPoint.position, shellEjectPoint.rotation);
        }
    }


    private void ShootSpread()
    {
        int bulletCount = 5;
        float spreadAngle = 30f;

        float startAngle = -spreadAngle * (bulletCount - 1) / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + spreadAngle * i;
            Quaternion rotation = Quaternion.Euler(0, 0, firePoint.eulerAngles.z + angle);
            Instantiate(shotgunBulletPrefab, firePoint.position, rotation);
        }
    }
}




/* public void Fire()
 {
     if (currentMode == GunMode.Normal)
     {
         Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
     }
     else if (currentMode == GunMode.Laser)
     {
         Instantiate(laserBulletPrefab, firePoint.position, firePoint.rotation);
     }
     else if (currentMode == GunMode.Shotgun)
     {
         ShootSpread();
     }
     else if (currentMode == GunMode.RapidFire)
     {
         Instantiate(rapidFireBulletPrefab, firePoint.position, firePoint.rotation);
     }

     // Tracer effect (optional)
     Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     mouseWorldPosition.z = 0f;
     WeaponTracer.Create(firePoint.position, mouseWorldPosition);

     // Muzzleflash
     if (muzzleFlashPrefab != null)
     {
         Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
     }

     // Shell Eject
     if (shellPrefab != null && shellEjectPoint != null)
     {
         Instantiate(shellPrefab, shellEjectPoint.position, shellEjectPoint.rotation);
     }
 }
*/