using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//test to ensure player is constructed with required components
public class PInstantiate
{
    [Test]
    public void ShouldHaveAllRequiredComponents()
    {
        //create player GameObject and add all expected components
        GameObject player = new GameObject("Player");
        player.AddComponent<CONTROLLER>();
        player.AddComponent<Move>();
        player.AddComponent<Jump>();
        player.AddComponent<PlayerStats>();
        player.AddComponent<Ground>();

        Assert.IsNotNull(player.GetComponent<CONTROLLER>(), "CONTROLLER component should be attached.");
        Assert.IsNotNull(player.GetComponent<Move>(), "Move component should be attached.");
        Assert.IsNotNull(player.GetComponent<Jump>(), "Jump component should be attached.");
        Assert.IsNotNull(player.GetComponent<PlayerStats>(), "PlayerStats component should be attached.");
        Assert.IsNotNull(player.GetComponent<Ground>(), "Ground component should be attached.");

        Object.DestroyImmediate(player);
    }
}
