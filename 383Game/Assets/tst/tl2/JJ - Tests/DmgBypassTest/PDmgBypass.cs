using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//dmg bypass tests to check that player damage processing respects BC cheat mode
public class PDmgBypass
{
     [Test]
    public void Damage_NotApplied_WhenBCModeEnabled()
    {
        GameObject player = new GameObject("Player");
        PlayerStats stats = player.AddComponent<PlayerStats>();

        stats.maxHealth = 100f;
        stats.health = 100f;

        //enable BCMode via PlayerPrefs
        PlayerPrefs.SetInt("BCMode", 1);

        //attempt to deal damage; should be bypassed
        stats.TakeDamage(20f);

        //verify health remains unchanged
        Assert.AreEqual(100f, stats.health, "Health should not decrease when BCMode is enabled.");

        //clean up PlayerPrefs
        PlayerPrefs.DeleteKey("BCMode");
        Object.DestroyImmediate(player);
    }
}
