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
    private Animator animator;

    private Transform currentGround;
    // Start is called before the first frame update
    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        animator.SetFloat("Y", rb.velocity.y);
        Debug.Log($"X: {rb.velocity.x} si Y: {rb.velocity.y}");
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
            animator.SetFloat("X", -rb.velocity.x);
        }
        if (horizontalDirection <= 0)
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);
            animator.SetFloat("X", -rb.velocity.x);
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
        Collider2D groundCol = Physics2D.OverlapBox(feetPosition.position, new Vector2(0.15f, 0.58f), 0f, groundLayer);
        Collider2D obstacleCol = Physics2D.OverlapBox(feetPosition.position, new Vector2(0.15f, 0.58f), 0f, obstacleLayer);
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
