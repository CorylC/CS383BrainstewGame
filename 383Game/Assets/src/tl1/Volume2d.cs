using UnityEngine;

public class Volume2d : MonoBehaviour
{
    public Transform listenerTransform;
    public AudioSource audioSource;
    public float minDist = 1;
    public float maxDist = 10;

    void Update()
    {
        float dist = Vector3.Distance(transform.position, listenerTransform.position);

        if (dist < minDist)
        {
            audioSource.volume = 1;
        }
        else if (dist > maxDist)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1 - ((dist - minDist) / (maxDist - minDist));
        }
    }
}
