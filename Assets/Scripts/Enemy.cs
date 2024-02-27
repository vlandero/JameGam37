using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 250f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float jumpProbability = .5f;
    private Rigidbody2D rb;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if(isActive && isOnScreen)
        {
            Move();
            RandomJump();
        }
    }
    protected override void Deactivate()
    {
        base.Deactivate();
        rb.isKinematic = true;
    }

    protected override void Activate()
    {
        base.Activate();
        rb.isKinematic = false;
    }

    private void Move()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void RandomJump()
    {
        if(Random.Range(0f, 100f) < jumpProbability && IsGrounded())
        {
            Debug.Log("Jumping");
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
    }

    private bool IsGrounded()
    {
        Collider2D groundCol = Physics2D.OverlapBox(feetPosition.position, new Vector2(0.1f, 0.1f), 0f, groundLayer);
        return groundCol != null;
    }
}
