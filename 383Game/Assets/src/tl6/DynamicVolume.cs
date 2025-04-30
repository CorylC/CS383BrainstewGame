using UnityEngine;

public class VolumeControlDynamicBindingBC
{
    public bool ShouldBeOn()
    {
        // Default behavior (virtual): toggle is OFF
        return false;
    }

    public void SetVolume(bool isOn)
    {
        // Optional: override if you want to store state
    }
}


public class VolumeControlDynamicBinding : VolumeControlDynamicBindingBC
{
    public bool ShouldBeOn()
    {
        // Override behavior: toggle is ON (muted)
        return true;
    }

    public void SetVolume(bool isOn)
    {
        // Optional: store mute state
    }
}

