using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DynamicLeverCollision : MonoBehaviour
{
    private LeverBase lever; //to use dynamic methods

    //[SerializeField] private UnityEvent _collisionEvent;
    [SerializeField] private string collideTag = "Player";

    void Start()
    {
        lever = new LeverDynamic(2); //init with scene ID 2
        //lever = new LeverBase(2); // then the non-overriden method called
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(collideTag))
        {
            lever.ChangeScene(); //dynamically bound method call ****** and called on collision

            //SceneManager.LoadScene(2);
            gameObject.SetActive(false);
        }
    }
}


/*

Since I still need some script to be able to attach to a unity object, 
I would have to use a monobehaviour script to call another script that doesn’t 
inherit from monobehaviour for dynamic binding

*/
