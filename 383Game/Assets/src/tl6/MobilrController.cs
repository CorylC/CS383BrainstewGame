using UnityEngine;

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
    }
}
