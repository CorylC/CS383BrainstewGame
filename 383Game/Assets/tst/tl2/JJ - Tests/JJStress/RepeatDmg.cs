using UnityEngine;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine.TestTools; //required for [UnityTest]
using System.Collections;

public class Stress_PlayerStatsTests : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;

    [UnityTest]
    public IEnumerator RepeatedDamageStressTest()
    {
        //load the freezer scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Freezer-S2");
        while (!asyncLoad.isDone)
        {
            yield return null; //wait until the scene is fully loaded
        }

        //wait one additional frame after loading
        yield return null;

        //find player obj
        player = GameObject.FindWithTag("Player");
        Assert.IsNotNull(player, "Player object not found in the scene!");

        //get the PlayerStats component since thats what we r testing
        playerStats = player.GetComponent<PlayerStats>();
        Assert.IsNotNull(playerStats, "PlayerStats component not found on Player object!");

        //set player health to full like spawn
        playerStats.health = playerStats.maxHealth = 100f;

        //sim multiple enemies attacking simultaneously
        float damagePerFrame = 50f; //increased damage per frame to simulate heavy load
        int framesToApplyDamage = 30; //apply damage for more frames

        Debug.Log("Starting stress test: Multiple enemies attacking simultaneously.");
        
        for (int i = 0; i < framesToApplyDamage; i++)
        {
            Debug.Log($"Frame {i + 1}: Applying damage.");

            //sim multiple enemies applying damage simultaneously
            for (int j = 0; j < 5; j++) //5 enemies attacking at once
            {
                if (player != null && playerStats != null) //chek if player still exists
                {
                    playerStats.TakeDamage(damagePerFrame);
                    Debug.Log($"Enemy {j + 1} applied {damagePerFrame} damage. Player health: {playerStats.health}");

                    //fail immediately if health goes below zero, but log it first for visibility
                    if (playerStats.health < 0f)
                    {
                        Debug.LogError($"FAIL: Player's health went below zero! Current health: {playerStats.health}");
                        Assert.Fail($"Player's health should never go below zero. Current health: {playerStats.health}");
                    }
                }
                else
                {
                    Debug.Log("Player has been destroyed or deactivated. Ending test.");
                    break;
                }
            }

            yield return null; //wait for next frame
        }

        Debug.Log("Stress test completed. Verifying outcomes...");

        //verify death logic triggers correctly when health reaches zero
        if (player == null || !player.activeSelf)
        {
            Debug.Log("Player is inactive or destroyed. Verifying scene transition...");
            Assert.AreEqual(4, SceneManager.GetActiveScene().buildIndex, "Game-over scene should load after death.");

            //sim lingering interactions after destruction (intentional failure point)
            Debug.Log("Attempting interaction after destruction...");
            try
            {
                playerStats.TakeDamage(10f); //this should fail if playerStats is invalid
                Debug.LogError("Interaction succeeded unexpectedly after destruction!");
            }
            catch (MissingReferenceException e)
            {
                Debug.Log($"Expected failure occurred: {e.Message}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Unexpected error occurred: {e.Message}");
                throw;
            }
        }
    }
}
