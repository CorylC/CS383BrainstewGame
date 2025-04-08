/*using UnityEngine;
using UnityEngine.UI;

public class MobileInputUI : MonoBehaviour
{
    public MobileInputController mobileInput;

    public Button jumpButton;
    public Button fastFallButton;
    public Button leftButton;
    public Button rightButton;

    private bool isLeftHeld = false;
    private bool isRightHeld = false;

    void Start()
    {
        // Button tap listeners
        jumpButton.onClick.AddListener(() => mobileInput.jumpPressed = true);

        // Fast fall logic
        fastFallButton.onClick.AddListener(() => {
            mobileInput.fastFallHeld = true;
            Invoke(nameof(ResetFastFall), 0.3f);
        });

        // Tap to move (can be extended to hold if needed)
        leftButton.onClick.AddListener(() => isLeftHeld = true);
        rightButton.onClick.AddListener(() => isRightHeld = true);
    }

    void Update()
    {
        float horizontal = 0;
        if (isLeftHeld) horizontal -= 1;
        if (isRightHeld) horizontal += 1;

        mobileInput.horizontalInput = horizontal;

        // Reset each frame
        isLeftHeld = false;
        isRightHeld = false;
    }

    void ResetFastFall()
    {
        mobileInput.fastFallHeld = false;
    }
}
*/