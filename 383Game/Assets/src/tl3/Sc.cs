using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon2D : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Where the projectile is spawned
    public float projectileSpeed = 10f; // Speed of the projectile
    public float spawnOffset = 0.5f; // Offset to avoid self-collision

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            CheckForEnemyClick();
        }
    }

    void CheckForEnemyClick()
    {
        // Get the mouse position in world space
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Perform a raycast to detect if an enemy was clicked
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy clicked: " + hit.collider.name);
            Shoot(hit.collider.transform.position);
        }
        else
        {
            Debug.Log("Clicked on " + (hit.collider != null ? hit.collider.name : "empty space"));
        }
    }

    void Shoot(Vector3 targetPosition)
    {
        Debug.Log("Shooting a projectile!");

        // Calculate direction from firePoint to the target position
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        // Rotate the firePoint to face the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Calculate the spawn position with an offset
        Vector3 spawnPosition = firePoint.position + direction * spawnOffset;

        // Create the projectile and set its direction
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
