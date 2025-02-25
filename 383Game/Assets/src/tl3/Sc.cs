using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon2D : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform firePoint; // The point from where the projectile will be fired
    public float projectileSpeed = 10f; // Speed of the projectile

    private void Update()
    {
        // Rotate the fire point to always face the mouse position
        RotateFirePoint();

        // Shoot when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Shoot();
        }
    }

    void RotateFirePoint()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the fire point to the mouse position
        Vector2 direction = mousePosition - firePoint.position;

        // Calculate the angle to rotate the fire point
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation to the fire point
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * projectileSpeed; // Ensure the projectile moves in the facing direction
        }

        Debug.Log("Projectile shot at angle: " + firePoint.rotation.eulerAngles.z);
    }
}
