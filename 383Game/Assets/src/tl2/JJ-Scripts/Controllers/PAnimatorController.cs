using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Ground))]
[RequireComponent(typeof(SpriteRenderer))]
public class PAnimatorController : MonoBehaviour
{
    Animator       _anim;
    Rigidbody2D    _body;
    Ground         _ground;
    SpriteRenderer _sprite;

    void Awake()
    {
        _anim   = GetComponent<Animator>();
        _body   = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1) Drive Speed parameter from horizontal velocity
        float speed = Mathf.Abs(_body.linearVelocity.x);
        _anim.SetFloat("Speed", speed);

        // 2) Drive IsJumping from ground check
        bool isJumping = !_ground.OnGround;
        _anim.SetBool("IsJumping", isJumping);

        // 3) Flip sprite based on ACTUAL MOTION (not raw input)
        if (_body.linearVelocity.x > 0.1f)
            _sprite.flipX = false;    // facing right
        else if (_body.linearVelocity.x < -0.1f)
            _sprite.flipX = true;     // facing left
    }

    public void TriggerDeath()
    {
        _anim.SetTrigger("Death");
    }
}
