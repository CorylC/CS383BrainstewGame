using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BP_BoundaryTests
{
    private GameObject bulletPrefab;
    private GameObject testCamera;

    [SetUp]
    public void Setup()
    {
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
    public IEnumerator MinimumBoundaryTest_OneBullet()
    {
        GameObject bullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        rb.linearVelocity = Vector2.right * 10f;


        bullet.tag = "Bullet";

        yield return new WaitForSeconds(1f);

        Assert.IsNotNull(bullet, "Bullet was not instantiated correctly.");
    }

    [UnityTest]
    public IEnumerator CollisionBoundaryTest_HighSpeedBullet()
    {
        GameObject bullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        rb.linearVelocity = Vector2.right * 100f;


        bullet.tag = "Bullet";

        yield return new WaitForSeconds(1f);

        Assert.IsNotNull(bullet, "Bullet did not instantiate or got destroyed prematurely.");
    }

   

    [TearDown]
    public void Cleanup()
    {
        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Object.Destroy(bullet);

        if (testCamera != null)
        {
            Object.Destroy(testCamera);
            testCamera = null;
        }
    }
}
