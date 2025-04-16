using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMoveInput(); //horizontal movement

    public abstract bool RetrieveJumpInput(); //jump action (vertical positive)

    public abstract bool RetrieveFastFallInput(); //fast fall action (vertical neg)
}

/*
    abstract base class is the interface for defining diff types of input handling methods
    its just nice for flexibility to switch between different input methods

    Abstract Class: defines input methods without implementation deets
    Scriptable Object: Allows creating asset instances in unity's editor
    Dynamic Binding: since the methods are abstract, it requires derived class
    to implement -- hence PlayerController.cs


    scriptable object just enables reusable input profiles in unity
*/
