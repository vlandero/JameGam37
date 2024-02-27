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
    private Animator animator;

    private bool hasTire = true;
    // Start is called before the first frame update
    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(SwitchTire());
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Y", rb.velocity.y);
        Debug.Log($"X: {rb.velocity.x} si Y: {rb.velocity.y}");

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
            animator.SetFloat("X", -rb.velocity.x);
        }
        if (horizontalDirection <= 0 && !IsWallLeft())
        {
            rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);
            animator.SetFloat("X", -rb.velocity.x);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(feetPosition.position, new Vector2(0.98f, 0.1f), 0f, groundLayer);
    }
    private bool IsWallLeft()
    {
        return Physics2D.OverlapBox(leftPosition.position, new Vector2(0.15f, 0.58f), 0f, groundLayer);
    }
    private bool IsWallRight()
    {
        return Physics2D.OverlapBox(rightPosition.position, new Vector2(0.15f, 0.58f), 0f, groundLayer);
    }

    IEnumerator SwitchTire()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            hasTire = !hasTire;
            animator.SetBool("hasTire", hasTire);
        }
    }
}
