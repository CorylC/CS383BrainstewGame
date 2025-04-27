using NUnit.Framework;
using UnityEngine;

public class BulletPierceLogic
{
    // Mock bullet with simple pierce mechanic
    private class MockBullet : MonoBehaviour
    {
        public int pierceCount = 2;

        public void HitEnemy()
        {
            pierceCount--;
            if (pierceCount <= 0)
                DestroyImmediate(gameObject); // Destroys the bullet after limit
        }
    }

    [Test]
    public void Bullet_Destroys_After_Piercing_Limit()
    {
        // Create bullet GameObject and attach mock logic
        GameObject bullet = new GameObject("Bullet");
        MockBullet b = bullet.AddComponent<MockBullet>();
        b.pierceCount = 2;

        // First hit: should still exist
        b.HitEnemy();
        Assert.IsFalse(b == null, "Bullet should still exist after 1st hit.");

        // Second hit: should be destroyed
        b.HitEnemy();

        // Use Unity's weird destroyed-object check
        Assert.IsTrue(b == null, "Bullet should be destroyed after piercing twice.");

        Debug.Log("✅ BulletPierceLogic passed.");
    }
}
