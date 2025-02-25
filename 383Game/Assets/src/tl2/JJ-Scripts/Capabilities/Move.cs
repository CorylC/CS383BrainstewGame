using UnityEngine;

[RequireComponent(typeof(CONTROLLER))]
public class Move : MonoBehaviour
{
    [SerializeField,Range(0f,100f)] private float _maxSpeed = 4f;
    [SerializeField,Range(0f,100f)] private float _maxAcceleration = 35f;
    [SerializeField,Range(0f,100f)] private float _maxAirAcceleration = 20f;

    private CONTROLLER _controller;
    private Vector2 _direction,_desiredVelocity,_velocity;
    private Rigidbody2D _body;
    private Ground _ground;

    private float _maxSpeedChange,_acceleration;
    private bool _onGround;

    public float _KBForce;
    public float _KBCounter;
    public float _KBTotalTime;

    public bool _HitFromRight;
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<CONTROLLER>();
    }


    void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x,0f) * Mathf.Max(_maxSpeed - _ground.Friction,0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.linearVelocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        if (_KBCounter <= 0)
        {
        _body.linearVelocity = _velocity;
        }
        else
        {
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
