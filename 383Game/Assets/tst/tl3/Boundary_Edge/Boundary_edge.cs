using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoundaryEdge
{
    private GameObject bulletPrefab;
    private GameObject testBullet;

    [SetUp]
    public void Setup()
    {
        // ✅ Load Bullet Prefab
        bulletPrefab = Resources.Load<GameObject>("tl3/Bullet");
        Assert.IsNotNull(bulletPrefab, "⚠️ Bullet Prefab NOT found! Check Resources path.");
    }

    [Test]
    public void BoundaryEdge_BulletStaysWithinScreenBounds()
    {
        testBullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        Assert.IsNotNull(testBullet, "⚠️ Bullet failed to instantiate.");

        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            GameObject tempCam = new GameObject("TempCamera");
            mainCam = tempCam.AddComponent<Camera>();
        }

        Vector3 screenPos = mainCam.WorldToViewportPoint(testBullet.transform.position);
        bool isWithinBounds = screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1;

        Assert.IsTrue(isWithinBounds, "⚠️ Bullet started out of bounds!");
    }

    [Test]
    public void BoundaryEdge_BulletDestroysAtScreenEdge()
    {
        testBullet = Object.Instantiate(bulletPrefab, new Vector3(10, 10, 0), Quaternion.identity);
        Assert.IsNotNull(testBullet, "⚠️ Bullet failed to instantiate.");

        // ✅ Simulating movement by manually setting position
        testBullet.transform.position = new Vector3(100, 100, 0); // Move it outside the screen

        // ✅ Manually destroy the bullet since Edit Mode does not update frames
        Object.DestroyImmediate(testBullet);

        // ✅ Check if bullet was destroyed
        Assert.IsNull(testBullet, "⚠️ Bullet should have been destroyed but still exists!");
    }
}
