using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


/*
This test checks if the MovingPlatform is bound to the correct movement range defined by posA and posB.
It ensures that the platform does not move outside the bounds set by these two points.
*/

public class PlatformBoundTest
{
    private MovingPlatform platform; // The platform to be tested
    private Transform posA, posB; // The two waypoints defining the movement range

    [OneTimeSetUp]
    public void Setup()
    {
        // Ensure the setup is not dependent on scene loading
        platform = null;
        posA = null;
        posB = null;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlatformMovementBoundTest()
    {
        SceneManager.LoadScene("Freezer-S2");

        // Wait for the scene to load
        yield return new WaitForSeconds(1f);  // Adjust wait time as needed

        // Find the platform and waypoints in the scene
        platform = platform = Object.FindFirstObjectByType<MovingPlatform>(); // Find the first instance of MovingPlatform in the scene
        posA = GameObject.Find("posA")?.transform; // Find the transform of posA
        posB = GameObject.Find("posB")?.transform; // Find the transform of posB

        // Ensure objects exist
        Assert.IsNotNull(platform, " Platform not found in scene!"); // Check if the platform is found
        Assert.IsNotNull(posA, " posA not found!"); // Check if posA is found
        Assert.IsNotNull(posB, " posB not found!"); // Check if posB is found

        // Wait for the platform to move
        yield return new WaitForSeconds(3f);

        // Get platform position
        Vector3 pos = platform.transform.position;

        // Define min/max values based on posA and posB
        float xMin = Mathf.Min(posA.position.x, posB.position.x);
        float xMax = Mathf.Max(posA.position.x, posB.position.x);
        float yMin = Mathf.Min(posA.position.y, posB.position.y);
        float yMax = Mathf.Max(posA.position.y, posB.position.y);

        // Check if the platform is within the movement range
        Assert.IsTrue(pos.x >= xMin && pos.x <= xMax, $" x out of bounds: {pos.x}");
        Assert.IsTrue(pos.y >= yMin && pos.y <= yMax, $" y out of bounds: {pos.y}");
    }
}
