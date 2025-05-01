using UnityEngine;

public class MobileShootButton : MonoBehaviour
{
    [Tooltip("Drag in the GameObject that has your teammate’s Shooting script")]
    public Shooting shootingScript;

    public void OnShootButtonPressed()
    {
        // Duplicate their firing logic using the same public fields
        if (shootingScript.canFire)
        {
            shootingScript.canFire = false;
            Instantiate(
                shootingScript.bullet,
                shootingScript.bulletTransform.position,
                Quaternion.identity
            );
            AudioManager.playSound(SoundType.SHOOT);
        }
    }
}

