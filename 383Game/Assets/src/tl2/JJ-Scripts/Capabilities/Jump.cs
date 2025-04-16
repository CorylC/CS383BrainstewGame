using UnityEngine;

[RequireComponent(typeof(CONTROLLER))] //CONTROLLER required on same game object for inputs

//VERTICAL MOVEMENT
public class Jump : MonoBehaviour
{
    [SerializeField,Range(0f,10f)] private float _jumpHeight = 3f;
    //height of each jump
    [SerializeField,Range(0,5)] private int _maxAirJumps = 0;
    //number of extra jumps allowed in air
    [SerializeField,Range(0f,5f)] private float _upwardMovementMultiplier = 1.7f;
    //gravity scale when moving up
    [SerializeField,Range(0f,5f)] private float _downwardMovementMultiplier = 3f;
    //gravity scale when moving down
    [SerializeField,Range(0f,10f)] private float _fastFallMultiplier = 2f;
    //extra gravity for fast-fall feature
    
    private CONTROLLER _controller;
    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity; //stores current velocity



    private int _jumpPhase; //tracks jump count (for air jumps)
    private float _defaultGravityScale,_jumpSpeed; //default gravity and jump speed

    private bool _desiredJump,_onGround; //jump input and ground state
    private bool _fastFalling = false; //fast-fall input


    //called before first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<CONTROLLER>();


        _defaultGravityScale = 1f;
    }

   //once per frame
    void Update()
    {
        _desiredJump |= _controller.input.RetrieveJumpInput(); //check jump input
        _fastFalling = _controller.input.RetrieveFastFallInput(); //check fast-fall input
    }

    //physics updates
    private void FixedUpdate()
    {
        _onGround = _ground.OnGround; //update ground state (every fixed interval)
    _velocity = _body.linearVelocity; //get current velocity

        if(_onGround)
        {
            _jumpPhase = 0; //reset jump phase when on the ground
        }
        if(_desiredJump)
        {
            _desiredJump = false;
            JumpAction(); //perform jump
        }

        //adjust gravity scale based on vertical velo and fast-fall
        if(_body.linearVelocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if(_body.linearVelocity.y < 0)
        {
            if (_fastFalling) //use downward multiplier if fast-falling (holding down key)
            {
                _body.gravityScale = _fastFallMultiplier * _downwardMovementMultiplier;
            }
            else
            {
                _body.gravityScale = _downwardMovementMultiplier;
            }
        }
        else if(_body.linearVelocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale; //not moving vertically, use default
        }

        _body.linearVelocity = _velocity;
}



    private void JumpAction() //handles actual jump logic, and air jumps
    {
        if (_onGround || _jumpPhase < _maxAirJumps){
            _jumpPhase += 1; //allow jump if on ground or there is an air jump

            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

            if(_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y,0f);
            }
            else if(_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_body.linearVelocity.y);
            }

            _velocity.y += _jumpSpeed;
            AudioManager.playSound(SoundType.JUMP); //jump sound bLOOP
        }
    }
}
