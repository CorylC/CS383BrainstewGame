using UnityEngine;

public class CONTROLLER : MonoBehaviour
{
    public InputController input = null; //reference to InputController
}


/*
this script bridges between player-specific capabilities (Move,Jump) and the
InputController's input system logic

this way it dynamically fetches inputs from the assigned InputController during runtime
which is nice for games so you can swap out different implementations without
having to modify the Move or Jump code

so CONTROLLER is responsible for handling player input via an InputController 
(scriptable object)
*/