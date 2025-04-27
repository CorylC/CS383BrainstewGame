using NUnit.Framework;
using UnityEngine;

public class BulletDamageZero
{
    // Simple mock enemy class with basic health and damage logic
    private class Enemy
    {
        public float health = 100f;

        // Subtracts damage from health
        public void TakeDamage(float dmg)
        {
            health -= dmg;
        }
    }

    [Test]
    public void Zero_Damage_Does_Not_Change_Health()
    {
        // Create a test enemy
        var enemy = new Enemy();

        // Apply 0 damage
        enemy.TakeDamage(0f);

        // Ensure health hasn't changed
        Assert.AreEqual(100f, enemy.health, "Health should stay the same with 0 damage.");

        // Log the test result
        Debug.Log("✅ BulletDamageZero passed.");
    }
}
