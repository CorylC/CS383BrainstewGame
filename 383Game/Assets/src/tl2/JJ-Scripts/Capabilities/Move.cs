using UnityEngine;

[RequireComponent(typeof(CONTROLLER))] //requires CONTROLLER on same object for input to resolve

//HORIZONTAL MOVEMENT
public class Move : MonoBehaviour
{
    [SerializeField,Range(0f,100f)] private float _maxSpeed = 4f; 
    //base max movement speed
    [SerializeField,Range(0f,100f)] private float _maxAcceleration = 35f;
    //max ground acceleration
    [SerializeField,Range(0f,100f)] private float _maxAirAcceleration = 20f;
    //max acceleration in the air 

    private CONTROLLER _controller;
    private Vector2 _direction,_desiredVelocity,_velocity;
    private Rigidbody2D _body; //physics
    private Ground _ground; //ref to ground for ground checks & friction

    private float _maxSpeedChange,_acceleration; //smooth acceleration
    private bool _onGround; //is on ground?

    //knockback
    public float _KBForce; //force of knockback
    public float _KBCounter; //time remaining for knockback effect
    public float _KBTotalTime; //total knockback duration

    public bool _HitFromRight; //direction

    //called when script instance is loaded
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<CONTROLLER>();
    }


    void Update() //get horizontal input & calculate desired velocity
    {
        _direction.x = _controller.input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x,0f) * Mathf.Max(_maxSpeed - _ground.Friction,0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.linearVelocity; //get current velo


        //choose acceleration based on ground/air state
        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;

        //smoothly move velocity.x towards desired velocity.x
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        if (_KBCounter <= 0)
        {
            //if not in knockback, apply final velocity
            _body.linearVelocity = _velocity;
        }
        else
        {
            //override velocity if in knockback
            if (_HitFromRight)
            {
                _body.linearVelocity = new Vector2(-_KBForce*2, _KBForce);
            }
            else
            {
                _body.linearVelocity = new Vector2(_KBForce*2, _KBForce);
            }

            _KBCounter -= Time.deltaTime;
        }

    }
}
