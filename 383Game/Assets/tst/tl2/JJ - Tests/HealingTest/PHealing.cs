using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//healing test to confirm health remains within bounds
public class PHealing
{
    [Test]
    public void Heal_DoesNotExceedMaxHealth()
    {
        GameObject player = new GameObject("Player");
        PlayerStats stats = player.AddComponent<PlayerStats>();

        //init health values
        stats.maxHealth = 100f;
        stats.health = 50f;

        //heaal with an amnt greater than the missing health
        stats.Heal(60f);

        //assert that health is capped at maxHealth
        Assert.AreEqual(100f, stats.health, "Health should not exceed maxHealth after healing.");

        Object.DestroyImmediate(player);
    }
}
