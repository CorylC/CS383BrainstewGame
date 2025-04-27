using NUnit.Framework;
using UnityEngine;

public class BulletExtremeGravity
{
    // Mock bullet to simulate gravity-based falling
    private class MockBullet : MonoBehaviour
    {
        public float gravityScale = 100f; // exaggerated gravity
        public Vector3 position = Vector3.zero;
        public float fallSpeed = 0f;

        // Simulate gravity over a fixed time step
        public void SimulateFall(float deltaTime)
        {
            // Increase fall speed based on gravity
            fallSpeed += gravityScale * deltaTime;

            // Update vertical position (falling down)
            position.y -= fallSpeed * deltaTime;
        }
    }

    [Test]
    public void Bullet_With_Insane_Gravity_Drops_Fast()
    {
        // Create test object
        var bulletObj = new GameObject("GravityBullet");
        var bullet = bulletObj.AddComponent<MockBullet>();

        // Simulate 0.1 seconds of falling
        bullet.SimulateFall(0.1f);

        // Bullet should have dropped significantly
        Assert.Less(bullet.position.y, -0.1f, "Bullet should have dropped significantly with high gravity.");
        Debug.Log("✅ BulletExtremeGravity passed.");

        // Clean up the test object
        Object.DestroyImmediate(bulletObj);
    }
}
