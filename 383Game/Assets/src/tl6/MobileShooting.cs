using UnityEngine;

public class MobileShootRelay : MonoBehaviour
{
    private Shooting shooting;

    void Start()
    {
        shooting = FindObjectOfType<Shooting>();
    }

    void Update()
    {
        if (PauseManager.isPaused || shooting == null)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Fire on touch began
            if (touch.phase == TouchPhase.Began && shooting.canFire)
            {
                shooting.canFire = false;
                Instantiate(shooting.bullet, shooting.bulletTransform.position, Quaternion.identity);
                AudioManager.playSound(SoundType.SHOOT);
            }
        }
    }
}
