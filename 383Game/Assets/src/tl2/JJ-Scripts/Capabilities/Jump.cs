using UnityEngine;

[RequireComponent(typeof(CONTROLLER))]
public class Jump : MonoBehaviour
{
    [SerializeField,Range(0f,10f)] private float _jumpHeight = 3f;
    [SerializeField,Range(0,5)] private int _maxAirJumps = 0;
    [SerializeField,Range(0f,5f)] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField,Range(0f,5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField,Range(0f,10f)] private float _fastFallMultiplier = 2f;
    
    private CONTROLLER _controller;
    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity;


    private int _jumpPhase;
    private float _defaultGravityScale,_jumpSpeed;

    private bool _desiredJump,_onGround;
    private bool _fastFalling = false;

    //this is start state before first frame update
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
        _desiredJump |= _controller.input.RetrieveJumpInput();
        _fastFalling = _controller.input.RetrieveFastFallInput();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
    _velocity = _body.linearVelocity;

        if(_onGround)
        {
            _jumpPhase = 0;
        }
        if(_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if(_body.linearVelocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if(_body.linearVelocity.y < 0)
        {
            if (_fastFalling)
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
            _body.gravityScale = _defaultGravityScale;
        }

        _body.linearVelocity = _velocity;
}



    private void JumpAction()
    {
        if (_onGround || _jumpPhase < _maxAirJumps){
            _jumpPhase += 1;

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
            AudioManager.playSound(SoundType.JUMP);
        }
    }
}
