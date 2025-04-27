using UnityEngine;
public class Gun : MonoBehaviour
{
    // Point in the scene where the bullet will spawn
    public Transform firePoint;

    // Bullet prefab to instantiate
    public GameObject bulletPrefab;

    //Muzzleflash effect to spawn when firing
    public GameObject muzzleFlashPrefab;

    // Shell prefab for visual shell ejection effect
    public GameObject shellPrefab;

    // Position where the shell casing is ejected from
    public Transform shellEjectPoint;

    void Update()
    {
        // Listen for left mouse click to trigger shooting
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Fire();
        }
    }

    public void Fire()
    {
        // Spawn a bullet at the fire point with the same rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Create a tracer effect from the gun to the mouse position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure Z is 0 for 2D
        WeaponTracer.Create(firePoint.position, mouseWorldPosition);

        //Create a muzzleflash at the firepoint
        if(muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        }

        // Eject a shell casing if the prefab and eject point are set
        if (shellPrefab != null && shellEjectPoint != null)
        {
            Instantiate(shellPrefab, shellEjectPoint.position, shellEjectPoint.rotation);
        }
    }
}
