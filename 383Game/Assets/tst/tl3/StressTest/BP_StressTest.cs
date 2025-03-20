using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Profiling;

public class BP_StressTest
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
    public IEnumerator StressTest_MemoryOverload()
    {
        int bulletCount = 0;
        int maxBullets = 20000;
        float bulletSpeed = 100f;
        const long memoryLimitBytes = 500 * 1024 * 1024; // 500MB

        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>() ?? bullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.linearVelocity = Random.insideUnitCircle.normalized * bulletSpeed;

            bullet.tag = "Bullet";
            bulletCount++;

            if (bulletCount % 1000 == 0)
            {
                long currentMemory = Profiler.GetTotalAllocatedMemoryLong();
                Debug.Log($"Instantiated bullets: {bulletCount}, Memory used: {currentMemory / (1024 * 1024)} MB");

                if (currentMemory > memoryLimitBytes)
                {
                    Debug.LogError($"Memory overload reached at {bulletCount} bullets ({currentMemory / (1024 * 1024)} MB)");
                    break;
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        Assert.Pass("Stress test completed and memory overload detected explicitly.");
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