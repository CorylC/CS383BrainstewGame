using NUnit.Framework;
using UnityEngine;

public class BulletZeroSpeedMovement
{
    // Mock bullet that updates its position based on speed
    private class MockBullet
    {
        public float speed = 0f;
        public Vector3 position = Vector3.zero;

        public void UpdatePosition()
        {
            position += Vector3.right * speed * Time.deltaTime;
        }
    }

    [Test]
    public void Bullet_With_Zero_Speed_Does_Not_Move()
    {
        var bullet = new MockBullet();
        bullet.UpdatePosition(); // Should stay still

        Assert.AreEqual(Vector3.zero, bullet.position, "Bullet should not move when speed is 0.");

        Debug.Log("✅ BulletZeroSpeedMovement passed.");
    }
}
