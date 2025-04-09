using System.Collections;
using System.Threading;
using UnityEngine;

public class BoxToss1 : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private GameObject player;
    private CannonShootDynamicBindingBC cannon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;
    private Vector3 originalPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cannon = new CannonShootDynamicBinding();  // instantiate it
        cannon.setSpeed(5);

        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 50)
        {
            timer += Time.deltaTime;
            if (timer > 4)
            {
                timer = 0;
                toss();
            }
        }


    }

    void toss()
    {
        AudioManager.playSound(SoundType.CANNONSHOOT);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Debug.Log("Bullet shot at speed: " + cannon.getSpeed());
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos; // Reset position
    }
}