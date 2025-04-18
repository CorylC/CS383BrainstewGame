using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//The types of sounds available, used when calling sounds
public enum SoundType
{
    JUMP,
    HURT,
    SHOOT,
    CANNONSHOOT,
    STARTGAME,
    BUTTONCLICK
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    //calls soundlist from scene
    public AudioManagerVolume audioVolume;
    [SerializeField] private AudioClip[] soundlist;
    public static AudioManager instance;
    private AudioSource audioSource;
    public static int AudioVolume = 1;

    //multiplier from audioSlider
    [Range(0.01f, 1f)]
    //[SerializeField] private float audioVolume = 1f;

    //quick list of ranges, maybe find better way to do this
    private int[] soundlistRanges = {0, 1, 4, 5, 6, 7};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    //gets audio sources on startup
    private void Start()
    {
        if (audioVolume == null)
        {
            audioVolume = new DynamicAudioManagerVolume();
        }
        audioSource = GetComponent<AudioSource>();
        instance.audioVolume.setVolume(1f);
    }

    //function to call if you want to play a sound
    //  SoundType sound is the enum of available sounds, you can call HURT to play hurt sounds or JUMP for jump sounds
    //  volume is set to 1 as a base so you do not need to add unless you want it to be more quiet
    public static void playSound(SoundType sound, float VOLUME = 1)
    {
        AudioClip soundNum;
        int rand;
        float volume = Mathf.Clamp(VOLUME, 0.01f, 1f);
        //maybe look for better ways?? Very not scalable way of introducing multiple audio samples per audio option
        switch (sound)
        {
            case SoundType.JUMP:
                rand = Random.Range(instance.soundlistRanges[0], instance.soundlistRanges[1]);
                soundNum = instance.soundlist[rand];
                break;
            case SoundType.HURT:
                rand = Random.Range(instance.soundlistRanges[1], instance.soundlistRanges[2]);
                soundNum = instance.soundlist[rand];
                break;
            case SoundType.SHOOT:
                rand = instance.soundlistRanges[2];
                soundNum = instance.soundlist[rand];
                break;
            case SoundType.CANNONSHOOT:
                rand = instance.soundlistRanges[3];
                soundNum = instance.soundlist[rand];
                break;
            case SoundType.STARTGAME:
                rand = instance.soundlistRanges[4];
                soundNum = instance.soundlist[rand];
                break;
            case SoundType.BUTTONCLICK:
                rand = instance.soundlistRanges[5];
                soundNum = instance.soundlist[rand];
                break;
            default:
                return;
        }

        //Plays the Sound
        //instance.audioVolume.setVolume(.5f);
        instance.audioSource.PlayOneShot(soundNum, volume * instance.audioVolume.getVolume());
    }

    /** Outdated method from before dynamic binding, included still for reference
    public void SetVolume(float volume)
    {
        audioVolume = Mathf.Clamp(volume, 0.01f, 1f);
    }
    */

    public void buttonSfx()
    {
        playSound(SoundType.BUTTONCLICK);
    }
}