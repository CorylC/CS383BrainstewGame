using UnityEngine;

public class VolumeControlDynamicBindingBC
{
    public virtual bool ShouldBeOn()
    {
        // Default behavior (virtual): toggle is OFF
        return false;
    }

    public virtual void SetVolume(bool isOn)
    {
        // Optional: override if you want to store state
    }
}


public class VolumeControlDynamicBinding : VolumeControlDynamicBindingBC
{
    public override bool ShouldBeOn()
    {
        // Override behavior: toggle is ON (muted)
        return true;
    }

    public override void SetVolume(bool isOn)
    {
        // Optional: store mute state
    }
}

