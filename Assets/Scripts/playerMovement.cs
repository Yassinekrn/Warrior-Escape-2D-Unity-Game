using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // GavityScale = 3 , Mass = 1, Terrain should be layered “Ground”    so that the player can Jump again

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 4.5f;
    public float speed = 5f; // adjust the speed of horizontal movement
    private Rigidbody2D rb; // reference to the Rigidbody2D component
    private float dirX = 0f;

    [Header("Jumping")]
    [SerializeField] private float VelJumpReleased = 2f; // adjust the fall speed after releasing the jump button
    [SerializeField] private float jumpForce = 10f;
    private int jumps = 0;

    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 10f;
    [SerializeField] private float _dashingTime = 0.18f;
    [SerializeField] private float _dashDelay = 1f;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash = true;
    private TrailRenderer _trailRenderer;

    [Header("Others")]
    [SerializeField] private LayerMask jumpableGround;
    
    private BoxCollider2D coll;
    private Animator _animator;
    
    private SpriteRenderer sprite;

    private enum animationStat { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // cache the Rigidbody2D component for performance
        _trailRenderer = GetComponent<TrailRenderer>();
        _animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // handle falling faster when jump button is released
        var jumpInputReleased = Input.GetButtonUp("Jump");
        if (jumpInputReleased && rb.velocity.y > 0)
        {
            // reduce the vertical velocity of the Rigidbody2D component to fall faster
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / VelJumpReleased);
        }

        // handle horizontal movement
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        // running and idle animation
        UpdateAnimation();

        // handle jumping movement
        if (Input.GetButtonDown("Jump") && jumps < 1)
        {
            // set the vertical velocity of the Rigidbody2D component to jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumps++;
        }
        isGrounded();
        // handle falling movement
        if (Input.GetKeyDown(KeyCode.S))
        {
            // set the vertical velocity of the Rigidbody2D component to fall faster
            rb.velocity = new Vector2(rb.velocity.x, -2);
        }
        //Dashing 
        var dashInput = Input.GetButtonDown("Dash");
        if (dashInput && _canDash)
        {
            _isDashing = true; // player is now dashing
            _canDash = false; // player cannot dash again until the dashing time is over
            _trailRenderer.emitting = true; // enable trail effect during dashing
            _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // set the direction of dashing based on player input
            if (_dashingDir == Vector2.zero) // if player input is not detected, set the direction of dashing to the direction the player is facing
            {
                _dashingDir = new Vector2(transform.localScale.x, 0);
            }
            //Stopping the dash
            StartCoroutine(StopDashing()); // start a coroutine to stop dashing after a certain duration
        }

        //_animator.SetBool("IsDashing", _isDashing);

        if (_isDashing)
        {
            rb.velocity = _dashingDir.normalized * _dashingVelocity; // set the velocity of the player to the direction and speed of dashing
            return; // exit the Update function to prevent other movement inputs from interfering with dashing
        }
    }
    // coroutine to stop dashing and reset dash ability
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        StartCoroutine(DashDelay()); // start a coroutine to delay dashing
    }

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(_dashDelay);
        _canDash = true; // allow dashing again after the delay time has passed
    }
    
    private void UpdateAnimation()
    {
        animationStat state;


        if (dirX > 0f)
        {
            state = animationStat.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = animationStat.running;
            sprite.flipX = true;
        }
        else
        {
            state = animationStat.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = animationStat.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = animationStat.falling;
        }
        _animator.SetInteger("state", (int)state);
    }
    
    private void isGrounded()
    {
        bool grounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        if (grounded)
        {
            jumps = 0;
        }

    }
}