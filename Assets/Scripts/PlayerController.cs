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
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private ObstacleManager obstacleManager;

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
        if(Input.GetButtonDown("Switch Reality"))
        {
            Debug.Log("Switch Reality");
            obstacleManager.SwitchReality();
        }
        if(Input.GetButtonDown("Stop Tire"))
        {
            Debug.Log("Stop Tire");
        }
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        bool isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
        else
        {
            AttachToFloor(isGrounded);
        }
    }
    void FixedUpdate()
    {
        if (horizontalDirection >= 0)
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);
        }
        if (horizontalDirection <= 0)
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);
        }
    }
    private void AttachToFloor(bool isGrounded)
    {
        if (isGrounded)
        {
            if (currentGround != null)
            {
                transform.parent = currentGround;
            }
        }
        else
        {
            transform.parent = null;
        }
    }
    private bool IsGrounded()
    {
        Collider2D groundCol = Physics2D.OverlapBox(feetPosition.position, new Vector2(0.98f, 0.1f), 0f, groundLayer);
        Collider2D obstacleCol = Physics2D.OverlapBox(feetPosition.position, new Vector2(0.98f, 0.1f), 0f, obstacleLayer);
        if (groundCol != null)
        {
            currentGround = groundCol.transform;
            return true;
        }
        else if (obstacleCol != null)
        {
            currentGround = obstacleCol.transform;
            return true;
        }
        currentGround = null;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("loss"))
        {
            Debug.Log("Game Over");
        }
        else if (collision.CompareTag("end"))
        {
            Debug.Log("You Win");
        }
    }
}
