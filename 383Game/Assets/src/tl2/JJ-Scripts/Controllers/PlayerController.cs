using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetKeyDown(KeyCode.W) ||
               Input.GetKeyDown(KeyCode.Space) ||
               Input.GetKeyDown(KeyCode.UpArrow);
    }//return true if any of the jump keys are pressed

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }//fetch horizontal movement (arrow keys or WASD)

    public override bool RetrieveFastFallInput()
    {
        return Input.GetKey(KeyCode.S) ||
               Input.GetKey(KeyCode.DownArrow);
    }//return if any of the fast fall keys are held 
}


/*
    this is essentially the concrete implementation of InputController
    defining how player inputs are mapped to input actions using Unity's built-in
    input system. inherits from InputController


    the public override explicitly implements the base class's abstract methods
    and Unity resolves which input controller to use at runtime for whatever
    is assigned to the ScriptableObject instance


    ORDER:
    
    let's say the user wants to move horizontally.

    they press the A key to move left.

    so:
        1. Input Retrieval (UnityEngine -> PlayerController)
        it'll return the keyboard input -1 raw from A

        2. PlayerController -> CONTROLLER(ref to InputController as it retrieved -1)
            makes the input instance
            dynamic binding resolves

        3. Movement Calculation (CONTROLLER -> Move) via Update
        4. Ground -> Move (check ground state)
        5. Physics (Move -> RigidBody2D) to get final velocity
*/