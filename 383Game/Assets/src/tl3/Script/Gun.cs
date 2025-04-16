using UnityEngine;


public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject shellPrefab;
    public Transform shellEjectPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Fire();
        }
    }


    void Fire()
    {
        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Weapon tracer effect
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        WeaponTracer.Create(firePoint.position, mouseWorldPosition);

        // Muzzle flash
        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        }

        // Shell ejection
        if (shellPrefab != null && shellEjectPoint != null)
        {
            Instantiate(shellPrefab, shellEjectPoint.position, shellEjectPoint.rotation);

        }
    }
}
