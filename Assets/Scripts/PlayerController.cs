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
    // Start is called before the first frame update
    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
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
        return Physics2D.OverlapBox(feetPosition.position, new Vector2(0.98f, 0.1f), 0f, groundLayer);
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
