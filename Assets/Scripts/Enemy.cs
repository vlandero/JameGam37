using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float jumpProbability = .5f;
    [SerializeField] private float jumpCooldown = 1f;

    private Rigidbody2D rb;
    private float lastJumpTime;
    private Vector2 lastVelocity;

    [HideInInspector] public Animator animator;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastJumpTime = -jumpCooldown;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        animator.SetFloat("Y", rb.velocity.y);
        if (isActive && isOnScreen)
        {
            Move();
            if (Time.time - lastJumpTime >= jumpCooldown)
            {
                RandomJump();
            }
        }
    }
    public override void Deactivate()
    {
        base.Deactivate();
        rb.isKinematic = true;
        animator.enabled = false;
        lastVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
    }

    public override void Activate()
    {
        base.Activate();
        rb.isKinematic = false;
        animator.enabled = true;
        rb.velocity = lastVelocity;
    }

    private void Move()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void RandomJump()
    {
        if (IsGrounded())
        {
            if (Random.Range(0f, 100f) < jumpProbability)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                lastJumpTime = Time.time;
                animator.SetTrigger("Jump");
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, 0.1f, groundLayer);
        if (hit.collider)
        {
            return true;
        }
        return false;
    }
}
