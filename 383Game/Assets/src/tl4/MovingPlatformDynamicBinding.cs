using UnityEngine;

public class MovingPlatformDynamicBindingBC
{
    public virtual float getSpeed()
    {
        return 0;
    }

    public virtual void setSpeed(float level)
    {

    }
}


public class MovingPlatformDynamicBinding : MovingPlatformDynamicBindingBC
{
    public float speed = 5f;
    public override float getSpeed()
    {
        return speed;
    }

    public override void setSpeed(float level)
    {
        speed = level;
    }
}
