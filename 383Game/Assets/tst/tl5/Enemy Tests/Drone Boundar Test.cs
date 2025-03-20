using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DroneTest
{
    private GameObject drone;
    private GameObject player;

    // Boundaries being defined
    private readonly Vector2 minBounds = new Vector2(-67f, -7f);
    private readonly Vector2 maxBounds = new Vector2(67f, 25f);

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load the scene 
        SceneManager.LoadScene("Freezer-S2");
        yield return null; // Wait a frame for the scene to load

        // Find Drone and Player objects
        drone = GameObject.Find("DroneGFX");
        Assert.IsNotNull(drone, "Drone GameObject not found in the scene!");

        player = GameObject.Find("Player"); 
        Assert.IsNotNull(player, "Player GameObject not found in the scene!");
    }

    [UnityTest]
    public IEnumerator Drone_StaysWithinBoundary_WhenChasingPlayer()
    {
        // Move the player out of bounds to trigger the drone's A* tracking behavior
        player.transform.position = new Vector2(100f, 30f); 

        // let drone move
        yield return new WaitForSeconds(10f); 

        // Check that the drone is still within the defined environment bounds
        Vector2 pos = drone.transform.position;
        Assert.IsTrue(pos.x >= minBounds.x && pos.x <= maxBounds.x, $"Drone X out of bounds: {pos.x}");
        Assert.IsTrue(pos.y >= minBounds.y && pos.y <= maxBounds.y, $"Drone Y out of bounds: {pos.y}");
    }
}
