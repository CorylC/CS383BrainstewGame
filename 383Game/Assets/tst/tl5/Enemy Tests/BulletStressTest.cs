using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BulletStressTest
{
    private GameObject bulletPrefab;
    private GameObject player;
    private const string prefabPath = "TrackingEnemies";
    private const int maxDrones = 500;
    private const float minPlayableFPS = 19f; // FPS cutoff threshold

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Freezer-S2");
        yield return null;  // Wait a frame for scene load

        bulletPrefab = Resources.Load<GameObject>(prefabPath);
        Assert.IsNotNull(bulletPrefab, $"Drone prefab not found at Resources/{prefabPath}. Please check the path.");
        player = GameObject.Find("Player"); 
        Assert.IsNotNull(player, "Player GameObject not found in the scene!");
        player.transform.position = new Vector2(100f, 30f); 
    }

    [UnityTest]
    public IEnumerator DroneStressTest_CreatesDronesUntilCrashOrLimit()
    {
        int count = 0;
        bool testFailed = false;
        string errorMessage = "Error";
        int batchSize = 100;

        while (!testFailed)
        {
            // Instantiate drones in batches
            for (int i = 0; i < batchSize; i++)
            {
                try
                {
                    Object.Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                    count++;
                }
                catch (System.Exception e)
                {
                    testFailed = true;
                    errorMessage = $"Stress test failed after spawning {count} drones. Exception: {e.Message}";
                    break;
                }
            }

            yield return null; // Allow Unity to update and process other things

            // Calculate FPS
            float currentFPS = 1f / Time.deltaTime;

            // If FPS drops below the threshold, stop the test
            if (currentFPS < minPlayableFPS)
            {
                testFailed = true;
                errorMessage = $"Stress test failed due to low FPS ({currentFPS:F2}). Instantiated {count} drones.";
                break;
            }

            // Log progress
            Debug.Log($"Successfully instantiated {count} drones. Current FPS: {currentFPS:F2}");

            // If you reach the maximum drones, stop the test
            if (count >= 100000)
                break;
        }

        if (testFailed)
        {
            Assert.Fail(errorMessage);
        }
        else
        {
            Debug.Log($"Stress test completed: Successfully instantiated {count} drones without crashing.");
            Assert.Pass();
        }
    }
}
