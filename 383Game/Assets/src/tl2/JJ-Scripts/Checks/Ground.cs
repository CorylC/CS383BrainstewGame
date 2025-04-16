using UnityEngine;


//a script dedicated to ground checking for the player
public class Ground : MonoBehaviour
{
    public bool OnGround{get; private set;} //read-only outside this script
    public float Friction{get; private set;} 
    //friction value of the surface player is standing on...read-only outside script

    private Vector2 _normal; //normal vector of contact point
    private PhysicsMaterial2D _material; //stores material of surface

    //called when the player collides with another collider
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false; //no longer on ground
        Friction = 0; //reset friction
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision); //see if collision is with ground
        RetrieveFriction(collision); //get friction value of surface
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision); //continuously check ground status
        RetrieveFriction(collision);
    }

    //checks all contact points in collision to determine if player on ground
    private void EvaluateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            _normal = collision.GetContact(i).normal; //get normal of contact point
            OnGround |= _normal.y >= 0.9f; //if normal mostly upwards, consider player on ground
        }
    }

    //function to get physics material info if any
    private void RetrieveFriction(Collision2D collision)
    {
        _material = collision.rigidbody.sharedMaterial;
        Friction = 0;

        if(_material != null)
        {
            Friction = _material.friction;
        }
    }
}
