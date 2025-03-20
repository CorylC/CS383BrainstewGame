using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TrapTriggerTest
{
    private GameObject player;
    private GameObject trap;

    [OneTimeSetUp]

    public void LoadScene()
    {
        SceneManager.LoadScene("Freezer-S2");
    }


    [UnityTest]
    public IEnumerator TrapActivatesForPlayer()
    {
        if (SceneManager.GetActiveScene().name != "Freezer-S2")
        {
            var sceneLoad = SceneManager.LoadSceneAsync("Freezer-S2");
            while (!sceneLoad.isDone)
            {
                yield return null; // Wait for scene to fully load
            }
        }

        yield return new WaitForSeconds(1f); // Wait for a frame to ensure objects have spawned

        player = GameObject.FindWithTag("Player");
        trap = GameObject.Find("Trap");

        Debug.Log($"[SETUP] Found Player: {player}");
        Debug.Log($"[SETUP] Found Trap: {trap}");

        if (player == null)
        {
            Debug.LogWarning("Player not found in scene. Creating a new one.");
            player = new GameObject("Player");
            player.AddComponent<BoxCollider2D>();
            player.AddComponent<Rigidbody2D>();
            player.tag = "Player";
            player.AddComponent<PlayerStats>();
        }

        if (trap == null)
        {
            Debug.LogWarning("Trap not found in scene. Creating a new one.");
            trap = new GameObject("Enemy");
            trap.AddComponent<BoxCollider2D>();
            trap.GetComponent<BoxCollider2D>().isTrigger = true;
            trap.AddComponent<TrapDamage>();
        }

        Assert.IsNotNull(player, "Player is missing from the scene.");
        Assert.IsNotNull(trap, "Trap is missing from the scene.");

        if (player == null || trap == null)
        {
            Assert.Fail("Player or Trap not found in scene.");
        }

        // Place player inside the trap's trigger area
        player.transform.position = trap.transform.position + Vector3.right; // Player in range

        // Wait for the trap to apply at least one instance of damage
        TrapDamage trapDamage = trap.GetComponent<TrapDamage>();
        yield return new WaitForSeconds(trap.GetComponent<TrapDamage>().DamageInterval + 0.1f);

        // Check if the player's health decreased
        var playerStats = player.GetComponent<PlayerStats>();
        Assert.IsTrue(playerStats.health < playerStats.maxHealth, "Player's health was not reduced after entering trap.");

        // Now, move the player out of the trigger range to ensure it stops triggering
        player.transform.position = trap.transform.position + Vector3.up; // Move out of range

        // Wait to check if damage stops
        yield return new WaitForSeconds(trap.GetComponent<TrapDamage>().DamageInterval + 0.1f);

        // Ensure no more damage is applied after leaving
        float healthAfterExit = playerStats.health;
        yield return new WaitForSeconds(trap.GetComponent<TrapDamage>().DamageInterval + 0.1f); // Wait for another interval
        Assert.AreEqual(healthAfterExit, playerStats.health, "Player continued taking damage after leaving the trap.");
        
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        if (player != null)
        {
            Debug.Log("Destroying Player.");
            Object.DestroyImmediate(player);
            player = null;
        }

        if (trap != null)
        {
            Debug.Log("Destroying Trap.");
            Object.DestroyImmediate(trap);
            trap = null;
        }
    }
}

