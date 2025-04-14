using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//knockback test to ensure external forces, such as enemy impacts, result in correct velocity changes
public class PKnockback
{
     //dmmy input controller that always returns zero (no input)
    public class DummyInput : InputController
    {
        public override float RetrieveMoveInput() => 0f;
        public override bool RetrieveJumpInput() => false;
        public override bool RetrieveFastFallInput() => false;
    }

    [Test]
    public void Knockback_AppliesCorrectForce_WhenHitFromRight()
    {
        GameObject player = new GameObject("Player");
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f;

  
        player.AddComponent<Ground>();

        //add CONTROLLER and assign dummy input
        CONTROLLER controller = player.AddComponent<CONTROLLER>();
        controller.input = ScriptableObject.CreateInstance<DummyInput>();

        //add Move component
        Move move = player.AddComponent<Move>();

        //set knockback variables manually
        move._KBCounter = 0.5f; //ensure knockback branch runs
        move._KBForce = 10f;    //set a sample force
        move._HitFromRight = true;

        //simulate physics update
        move.FixedUpdate();

        //expected knockback velocity when hit from the right:
        //new Vector2(-_KBForce*2, _KBForce) => (-20, 10)
        Vector2 expected = new Vector2(-20f, 10f);
        Assert.AreEqual(expected.x, rb.linearVelocity.x, 0.01f, "Knockback force X component is incorrect.");
        Assert.AreEqual(expected.y, rb.linearVelocity.y, 0.01f, "Knockback force Y component is incorrect.");

        Object.DestroyImmediate(player);
    }
}
