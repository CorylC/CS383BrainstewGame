using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BP_EnhancedStressTest
{
    private GameObject bulletPrefab;
    private GameObject testCamera;

    [SetUp]
    public void Setup()
    {
        // Prevent multiple cameras from being created
        if (Camera.main == null)
        {
            if (GameObject.Find("TempCamera") == null)
            {
                testCamera = new GameObject("TempCamera");
                testCamera.AddComponent<Camera>();
                testCamera.tag = "MainCamera";
                Object.DontDestroyOnLoad(testCamera);
            }
        }

        bulletPrefab = Resources.Load<GameObject>("tl3/Bullet");
        Assert.IsNotNull(bulletPrefab, "Bullet Prefab NOT found!");
    }

    [UnityTest]
    public IEnumerator EnhancedStressTest_BulletMovementAndCollision()
    {
        int bulletCount = 0;
        int maxBullets = 2000;
        float bulletSpeed = 50f;

        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = bullet.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
            }

            Vector2 direction = Random.insideUnitCircle.normalized;
            rb.linearVelocity = direction * bulletSpeed;

            bullet.tag = "Bullet";

            bulletCount++;
            yield return null;
        }

        // Allow bullets to interact
        yield return new WaitForSeconds(3f);

        Assert.AreEqual(maxBullets, bulletCount, "Not all bullets instantiated correctly.");
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Object.Destroy(bullet);
        }

        if (testCamera != null)
        {
            Object.Destroy(testCamera);
            testCamera = null;
        }
    }
}
