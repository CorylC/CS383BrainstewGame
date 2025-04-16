using UnityEngine;


public class AudioManagerVolume
{
    public virtual float getVolume()
    {
        Debug.Log("Using the static form of binding, setting to 1f");
        return 1f;
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
        Debug.Log("Using the dynamic form of binding, setting to " + volume);
        return volume;
    }

    public override void setVolume(float VOLUME)
    {
        volume = Mathf.Clamp(VOLUME, 0.01f, 1f);
    }
}