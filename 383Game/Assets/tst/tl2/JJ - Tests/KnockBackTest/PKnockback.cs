using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//knockback test to ensure external forces, such as enemy impacts, result in correct velocity changes

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
