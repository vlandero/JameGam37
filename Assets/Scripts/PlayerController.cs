using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 300f;

    [SerializeField]
    private Transform feetPosition;
    [SerializeField]
    private Transform leftPosition;
    [SerializeField]
    private Transform rightPosition;
    [SerializeField]
    private LayerMask groundLayer;

    private float horizontalDirection;
    private Rigidbody2D rb;
    private Transform currentGround;
    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        currentGround = null;
    }

    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        bool isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
        else if (isGrounded)
        {
            if(currentGround != null)
            {
                this.transform.parent = currentGround;
            }
        }
        else
        {
            this.transform.parent = null;
        }
    }
    void FixedUpdate()
    {
        if (horizontalDirection >= 0 && !IsWallRight())
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);
        }
        if (horizontalDirection <= 0 && !IsWallLeft())
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);
        }
    }
    private bool IsGrounded()
    {
        Collider2D groundCol = Physics2D.OverlapBox(feetPosition.position, new Vector2(0.98f, 0.1f), 0f, groundLayer);
        if(groundCol != null)
        {
            currentGround = groundCol.transform;
            return true;
        }
        currentGround = null;
        return false;
    }
    private bool IsWallLeft()
    {
        return Physics2D.OverlapBox(leftPosition.position, new Vector2(0.1f, 0.98f), 0f, groundLayer);
    }
    private bool IsWallRight()
    {
        return Physics2D.OverlapBox(rightPosition.position, new Vector2(0.1f, 0.98f), 0f, groundLayer);
    }
}
