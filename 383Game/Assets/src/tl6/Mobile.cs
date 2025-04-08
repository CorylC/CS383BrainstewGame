/*using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class MobileInputController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetKeyDown(KeyCode.W) ||
               Input.GetKeyDown(KeyCode.Space) ||
               Input.GetKeyDown(KeyCode.UpArrow);
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveFastFallInput()
    {
        return Input.GetKey(KeyCode.S) ||
               Input.GetKey(KeyCode.DownArrow);
    }
}
*/