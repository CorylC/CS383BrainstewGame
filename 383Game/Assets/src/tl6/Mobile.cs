using UnityEngine;

<<<<<<< Updated upstream
public class MobileInputUI : MonoBehaviour
=======
<<<<<<< Updated upstream
[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class MobileInputController : InputController
>>>>>>> Stashed changes
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
=======
public class MobileInputUIManager : MonoBehaviour
{
    public static MobileInputUIManager Instance;

    [HideInInspector] public bool isJumpPressed;
    [HideInInspector] public int moveDirection; // -1 = left, 1 = right, 0 = idle

    private void Awake()
    {
        Instance = this;
    }

    // Called by UI buttons
    public void JumpButtonDown() => isJumpPressed = true;
    public void JumpButtonUp() => isJumpPressed = false;

    public void LeftButtonDown() => moveDirection = -1;
    public void RightButtonDown() => moveDirection = 1;
    public void MoveButtonUp() => moveDirection = 0;
>>>>>>> Stashed changes
}
