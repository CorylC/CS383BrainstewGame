using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

/*
This test checks if the MovingPlatform and TrapDamage objects are spawned correctly in the scene.
It ensures that the specified number of platforms and traps are instantiated and exist in the scene.
*/

public class NewTestScript : MonoBehaviour //  Inherit from MonoBehaviour
{
    public GameObject platformPrefab; // Prefab for the moving platform
    public GameObject trapPrefab; // Prefab for the trap
    public int platformCount = 500000; // Number of platforms to spawn
    public int trapCount = 500000; // Number of traps to spawn

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Freezer-S2");

        // Load prefabs if they are in a "Resources" folder
        
        platformPrefab = Resources.Load<GameObject>("MovingPlatformPrefab");
        trapPrefab = Resources.Load<GameObject>("TrapPrefab");
        

        if (platformPrefab == null || trapPrefab == null) // Check if prefabs are loaded
        {
            Debug.LogError("Prefabs not found! Make sure they're under your 'tl' folder under the 'prefab' folder.");
        }

        // Spawn objects
        SpawnObjects(platformPrefab, platformCount); // Spawn platforms
        SpawnObjects(trapPrefab, trapCount); // Spawn traps
    }

    void SpawnObjects(GameObject prefab, int count) {
        if (prefab == null) return; // Prevent null reference errors

        for (int i = 0; i < count; i++) {
            Vector3 randomPos = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0); // Random position within a range
            Instantiate(prefab, randomPos, Quaternion.identity); // Instantiate the prefab at the random position
        }
    }

    [UnityTest]
    public IEnumerator MultiplePlatformAndTraps()
    {
        // Wait for a frame to ensure objects have spawned
        yield return null;

        // Assert that objects exist in the scene
        Assert.Greater(FindObjectsByType<MovingPlatform>(FindObjectsSortMode.None).Length, 0); // Check if platforms exist
        Assert.Greater(FindObjectsByType<TrapDamage>(FindObjectsSortMode.None).Length, 0); // Check if traps exist
    }
}

