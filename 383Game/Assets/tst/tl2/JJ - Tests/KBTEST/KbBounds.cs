using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//knockback test to make sure external forces, such as enemy impacts, result in correct velocity changes

public class PKnockback
{
    public class DummyInput : InputController
    {
        public override float RetrieveMoveInput() => 0f;
        public override bool RetrieveJumpInput() => false;
        public override bool RetrieveFastFallInput() => false;
    }

    [UnityTest]
    public IEnumerator Knockback_AppliesCorrectForce_WhenHitFromRight()
    {
        GameObject player = new GameObject("Player");
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        player.AddComponent<Ground>();

        CONTROLLER controller = player.AddComponent<CONTROLLER>();
        controller.input = ScriptableObject.CreateInstance<DummyInput>();

        Move move = player.AddComponent<Move>();
        move._KBCounter = 0.5f;
        move._KBForce = 10f;
        move._HitFromRight = true;

        yield return new WaitForFixedUpdate();

        Vector2 expected = new Vector2(-20f, 10f);
        Assert.AreEqual(expected.x, rb.linearVelocity.x, 0.01f, "X component");
        Assert.AreEqual(expected.y, rb.linearVelocity.y, 0.01f, "Y component");

        Object.DestroyImmediate(player);
    }
}


/*
TEST PLAN

•	Boundary test for minimum speed
•	Boundary test for maximum speed
•	Stress test for repeated simultaneous damage to player health from multiple enemies
•	Instantiation test to make sure player is constructed with all required components
•	Jump test to verify input is indeed translated into upward movement
•	Healing test to confirm health remains within bounds and doesn’t go beyond max
•	Knockback test to make sure external forces, like enemy impacts, result in correct velocity changes
•	Damage bypass test to make sure player damage processing respects BC cheat mode


So far none of my currently implemented tests have found a bug in my code due to a teammate adding 
code or game objects. Though some tests I have could let me or a teammate know if that happens 
– for example my stress test RepeatDmg.cs can be ran when my teammate adds new enemy behaviors or 
modes of attack that send damage to the player. In the case that damage is done extremely rapidly in 
succession or something is miscalculated causing the player’s health to drop below zero or skip the 
player death sequence the test would detect that because player’s health should never go negative and
the game-over scene should be triggered correctly without crashing
*/