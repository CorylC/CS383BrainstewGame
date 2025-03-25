using UnityEngine;


public class AudioManagerVolume
{


    public virtual float getVolume()
    {
        return 1;
    }

    public virtual void setVolume(float VOLUME)
    {

    }
}

public class DynamicAudioManagerVolume : AudioManagerVolume
{
    public float volume = 1f;
    public override float getVolume()
    {
        return volume;
    }

    public override void setVolume(float VOLUME)
    {
        volume = Mathf.Clamp(VOLUME, 0.01f, 1f);
    }
}