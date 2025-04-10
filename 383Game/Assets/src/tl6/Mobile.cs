using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    public MobileInputController inputController;

    public void OnJumpPressed()
    {
        inputController.jumpPressed = true;
    }

    public void OnFastFallDown()
    {
        inputController.fastFallHeld = true;
    }

    public void OnFastFallUp()
    {
        inputController.fastFallHeld = false;
    }

    public void MoveLeftDown()
    {
        inputController.horizontalInput = -1f;
    }

    public void MoveRightDown()
    {
        inputController.horizontalInput = 1f;
    }

    public void MoveReleased()
    {
        inputController.horizontalInput = 0f;
    }
}
