using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    JUMP,
    HURT
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundlist;
    private static AudioManager instance;
    private AudioSource audioSource;
    public static int AudioVolume = 1;

    [Range(0.01f, 1f)]
    [SerializeField] private float audioVolume = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void playSound(SoundType sound, float volume = 1)
    {
        AudioClip soundNum;
        if (sound == SoundType.HURT)
        {
            int rand = Random.Range(1, 4);
            Debug.Log("The random is: " + rand);
            soundNum = instance.soundlist[rand];
        }
        else
        {
            soundNum = instance.soundlist[(int)sound];
        }
        instance.audioSource.PlayOneShot(soundNum, volume * instance.audioVolume);
    }

    public void SetVolume(float volume)
    {
        audioVolume = Mathf.Clamp(volume, 0.01f, 1f);
    }
}
