using UnityEngine;

public class MovingPlatformDynamicBindingBC
{
    public float getSpeed()
    {
        return 4;
    }

    public void setSpeed(float level)
    {

    }
}


public class MovingPlatformDynamicBinding : MovingPlatformDynamicBindingBC
{
    public float speed = 5f;
    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float level)
    {
        speed = level;
    }
}
