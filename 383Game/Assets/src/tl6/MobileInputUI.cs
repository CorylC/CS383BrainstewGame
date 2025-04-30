using UnityEngine;

<<<<<<< Updated upstream
// This makes the class creatable as a ScriptableObject via the Unity editor.
[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class MobileInputController : InputController
=======
<<<<<<< Updated upstream
public class MobileInputUI : MonoBehaviour
>>>>>>> Stashed changes
{
    // Public fields for UI buttons to modify
    public bool jumpPressed = false;  // Set to true when jump button is pressed on mobile UI
    public bool fastFallHeld = false; // Set to true when fast fall button is held on mobile UI
    public float horizontalInput = 0f; // Set by mobile UI to simulate left/right movement

    // Public methods to modify the fields, for linking with UI buttons
    public void SetJumpPressed(bool value)
    {
        jumpPressed = value;
    }

    public void SetFastFallHeld(bool value)
    {
        fastFallHeld = value;
    }

    public void SetHorizontalInput(float value)
    {
<<<<<<< Updated upstream
        horizontalInput = value;
    }

    // Checks for jump input from either the mobile UI or keyboard.
    public override bool RetrieveJumpInput()
    {
        bool result = jumpPressed ||
                      Input.GetKeyDown(KeyCode.W) ||
                      Input.GetKeyDown(KeyCode.Space) ||
                      Input.GetKeyDown(KeyCode.UpArrow);

        jumpPressed = false;  // Reset to prevent multiple triggers
        return result;
    }

    // Checks for horizontal movement input.
    public override float RetrieveMoveInput()
    {
        if (horizontalInput != 0)
        {
            return horizontalInput;  // Return mobile input if available
        }

        return Input.GetAxisRaw("Horizontal");  // Otherwise, fallback to keyboard input
    }

    // Checks for fast fall input from mobile UI or keyboard.
    public override bool RetrieveFastFallInput()
    {
        return fastFallHeld ||
               Input.GetKey(KeyCode.S) ||
               Input.GetKey(KeyCode.DownArrow);
=======
        mobileInput.fastFallHeld = false;
=======
[CreateAssetMenu(fileName = "MobileController", menuName = "InputController/MobileController")]
public class MobileController : InputController
{
    public override bool RetrieveJumpInput()
    {
        // Return and then reset, to mimic single-tap jump
        if (MobileInputUIManager.Instance.isJumpPressed)
        {
            MobileInputUIManager.Instance.isJumpPressed = false;
            return true;
        }
        return false;
    }

    public override float RetrieveMoveInput()
    {
        return MobileInputUIManager.Instance.moveDirection;
    }

    public override bool RetrieveFastFallInput()
    {
        return false; // No fast fall for now (will become shoot later)
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    }
}
