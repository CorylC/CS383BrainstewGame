using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JmpBoundary
{
    public class DummyJumpInput : InputController
    {
        public override float RetrieveMoveInput() => 0f;
        public override bool RetrieveJumpInput() => true;
        public override bool RetrieveFastFallInput() => false;
    }

    [UnityTest]
    public IEnumerator Jump_IncreasesUpwardVelocity()
    {
        //setup
        GameObject player = new GameObject("Player");
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        player.AddComponent<Ground>();
        
        CONTROLLER controller = player.AddComponent<CONTROLLER>();
        DummyJumpInput dummyInput = ScriptableObject.CreateInstance<DummyJumpInput>();
        controller.input = dummyInput;
        
        Jump jump = player.AddComponent<Jump>();

        //let unity handle Update/FixedUpdate calls naturally
        yield return null; //triggers Update()
        yield return new WaitForFixedUpdate();

        //assert
        Assert.Greater(rb.linearVelocity.y, 0f);
        Object.DestroyImmediate(player);
    }
}
