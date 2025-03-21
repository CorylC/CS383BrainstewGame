using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class NewTestScript : MonoBehaviour //  Inherit from MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject trapPrefab;
    public int platformCount = 500000;
    public int trapCount = 500000;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Freezer-S2");

        // Load prefabs if they are in a "Resources" folder
        
        platformPrefab = Resources.Load<GameObject>("MovingPlatformPrefab");
        trapPrefab = Resources.Load<GameObject>("TrapPrefab");
        

        if (platformPrefab == null || trapPrefab == null)
        {
            Debug.LogError("Prefabs not found! Make sure they're under your 'tl' folder under the 'prefab' folder.");
        }

        // Spawn objects
        SpawnObjects(platformPrefab, platformCount);
        SpawnObjects(trapPrefab, trapCount);
    }

    void SpawnObjects(GameObject prefab, int count) {
        if (prefab == null) return; // Prevent null reference errors

        for (int i = 0; i < count; i++) {
            Vector3 randomPos = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
            Instantiate(prefab, randomPos, Quaternion.identity);
        }
    }

    [UnityTest]
    public IEnumerator MultiplePlatformAndTraps()
    {
        // Wait for a frame to ensure objects have spawned
        yield return null;

        // Assert that objects exist in the scene
        Assert.Greater(FindObjectsByType<MovingPlatform>(FindObjectsSortMode.None).Length, 0);
        Assert.Greater(FindObjectsByType<TrapDamage>(FindObjectsSortMode.None).Length, 0);
    }
}

