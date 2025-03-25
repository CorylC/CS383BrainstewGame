using UnityEngine;

public class CannonShootDynamicBindingBC
{
    

    public virtual float getSpeed()
    {
        return 4;
    }

    public virtual void setSpeed(float level)
    {
        
    }
}

public class CannonShootDynamicBinding : CannonShootDynamicBindingBC
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
