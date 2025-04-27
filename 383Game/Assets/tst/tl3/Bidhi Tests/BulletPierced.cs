using NUnit.Framework;
using UnityEngine;

public class BulletSpeedClamp
{
    // Mock bullet class that simulates clamping speed
    private class MockBullet : MonoBehaviour
    {
        public float speed;
        public float maxSpeed = 50f;

        // Clamp speed so it never exceeds maxSpeed
        public void ApplySpeed(float inputSpeed)
        {
            speed = Mathf.Min(inputSpeed, maxSpeed);
        }
    }

    [Test]
    public void Bullet_Speed_Is_Clamped_To_Max()
    {
        // Create a test bullet GameObject
        GameObject bullet = new GameObject("Bullet");
        var b = bullet.AddComponent<MockBullet>();

        // Apply an extremely high speed
        b.ApplySpeed(100f); // way above the allowed max

        // Ensure speed is clamped to 50
        Assert.AreEqual(50f, b.speed, "Bullet speed should be clamped to max.");

        Debug.Log("✅ BulletSpeedClamp passed.");
    }
}
