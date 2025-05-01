using UnityEngine;

public class PowerupFloat : MonoBehaviour
{
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;
    public float rotateSpeed = 30f;
    public float glowPulseSpeed = 2f;
    public float glowMin = 0.6f;
    public float glowMax = 1.2f;

    private Vector3 startPos;
    private SpriteRenderer sr;
    private float glowTime = 0f;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Floating up and down
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);

        // Rotate
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Glow pulse
        if (sr != null)
        {
            glowTime += Time.deltaTime * glowPulseSpeed;
            float glow = Mathf.Lerp(glowMin, glowMax, (Mathf.Sin(glowTime) + 1f) / 2f);
            sr.color = new Color(glow, glow, glow, 1f);
        }
    }
}
