using UnityEngine;
using UnityEngine.UI;

// Base class for volume control (not a MonoBehaviour)
public class VolumeControlBase
{
    protected float baseVolume = 10f; // Base volume level

    public virtual float GetVolume()
    {
        return baseVolume;
    }

    public virtual void SetVolume(float volume)
    {
        baseVolume = volume;
    }
}

// Derived class for specific volume behavior
public class GameVolumeControl : VolumeControlBase
{
    private float gameVolume = 50f; // Specific game volume

    public override float GetVolume()
    {
        return gameVolume; // Returns the specific game volume
    }

    public override void SetVolume(float volume)
    {
        gameVolume = volume;
    }
}

