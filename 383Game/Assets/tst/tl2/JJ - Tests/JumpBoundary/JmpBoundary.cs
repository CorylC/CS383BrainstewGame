using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//jump capability tests to verify input is correctly translated into upward movement
public class JmpBoundary
{
    //dummy implementation of InputController to simulate constant jump input
    public class DummyJumpInput : InputController
    {
        public override float RetrieveMoveInput() => 0f;
        public override bool RetrieveJumpInput() => true; //alwas return jump
        public override bool RetrieveFastFallInput() => false;
    }

    [Test]
    public void Jump_IncreasesUpwardVelocity()
    {
        //create player and add req comp
        GameObject player = new GameObject("Player");
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f;

        //add ground componen
        player.AddComponent<Ground>();

        //add CONTROLLER and assign dummy inpt
        CONTROLLER controller = player.AddComponent<CONTROLLER>();
        DummyJumpInput dummyInput = ScriptableObject.CreateInstance<DummyJumpInput>();
        controller.input = dummyInput;

        //add jump component
        Jump jump = player.AddComponent<Jump>();

        //sim one frame of input and physics
        jump.Update();
        jump.FixedUpdate();

        //chek that rigidbodys vertical velocity increases
        Assert.Greater(rb.linearVelocity.y, 0f, "Player should have upward velocity after jump input.");

        Object.DestroyImmediate(player);
    }
}
