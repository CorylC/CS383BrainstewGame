using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon2D : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // The point where the projectile will be instantiated
    public float projectileSpeed = 10f; // Speed of the projectile

    private void Update()
    {
        // Shoot with the left mouse button (avoiding space bar conflicts)
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Shooting a projectile!");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Make the projectile shoot straight to the right
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.right * projectileSpeed;
        }
    }
}
