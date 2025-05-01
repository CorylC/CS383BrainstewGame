using UnityEngine;

[CreateAssetMenu(fileName = "MobileController", menuName = "InputController/MobileController")]
public class MobileController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return MobileInputUIManager.Instance.isJumpPressed;
    }

    public override float RetrieveMoveInput()
    {
        return MobileInputUIManager.Instance.moveDirection;
    }

    public override bool RetrieveFastFallInput()
    {
        return false; // Replaced with shoot button logic (not needed for now)
    }
}
