using UnityEngine;

public class ShotgunGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shotgunBulletPrefab;

    public int bulletCount = 5;
    public float spreadAngle = 30f;
    public float shootCooldown = 0.3f;

    private float lastShootTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= shootCooldown)
        {
            ShootSpread();
            lastShootTime = Time.time;
        }
    }

    void ShootSpread()
    {
        if (shotgunBulletPrefab == null) return;

        float angleStep = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle * 0.5f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            float totalAngle = firePoint.eulerAngles.z + angle;

            // Create direction from angle
            Vector2 direction = Quaternion.Euler(0, 0, totalAngle) * Vector2.right;

            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 15f; // bullet speed here
            }
        }
    }


}
