using UnityEngine;

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
}

