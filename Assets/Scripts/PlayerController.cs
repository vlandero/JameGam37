using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float airSpeed = 2f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float stepHeight = 0.15f;
    [SerializeField] private float stepCheckDistance = 0.3f;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private InitializeBackgrounds panel;

    public ObstacleManager obstacleManager;

    [SerializeField] private float switchRealityCooldown = 5f;
    [SerializeField] private float stopTireCooldown = 5f;
    [SerializeField] private TextMeshProUGUI switchRealityText;
    [SerializeField] private TextMeshProUGUI stopTireText;

    private float switchRealityTimer = 0f;
    private float stopTireTimer = 0f;

    private float currentSpeed;

    private float horizontalDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentGround;

    public SpriteRenderer spriteRenderer;

    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = speed;
        currentGround = null;
        animator.SetBool("hasTire", true);
    }
   
    void Update()
    {
        switchRealityTimer -= Time.deltaTime;
        stopTireTimer -= Time.deltaTime;

        stopTireText.text = "You can stop the tire in: " + Mathf.Clamp(stopTireTimer, 0, stopTireCooldown).ToString("F2") + "s";
        switchRealityText.text = "You can switch reality in: " + Mathf.Clamp(switchRealityTimer, 0, switchRealityCooldown).ToString("F2") + "s";
        if (Input.GetButtonDown("Switch Reality") && switchRealityTimer <= 0f)
        {
            obstacleManager.SwitchReality();
            switchRealityTimer = switchRealityCooldown;
        }
        if (Input.GetButtonDown("Stop Tire") && stopTireTimer <= 0f)
        {
            StopTire();
            Invoke(nameof(StartTire), 2f);
            stopTireTimer = stopTireCooldown;
        }
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        bool isGrounded = IsGrounded();

        animator.SetFloat("Y", rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            AttachToFloor(isGrounded);
        }
    }
    public void StopTire()
    {
        animator.SetBool("hasTire",false);
        panel.currentRollingSpeed = 0;
        panel.currentTireRollingSpeed = 0;
    }
    public void StartTire()
    {
        animator.SetBool("hasTire", true);
        panel.currentRollingSpeed = panel.rollingSpeed;
        panel.currentTireRollingSpeed = panel.tireRollingSpeed;
    }
    void FixedUpdate()
    {
        if(IsGrounded())
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = airSpeed;
        }

        rb.velocity = new Vector2(horizontalDirection * currentSpeed, rb.velocity.y);
        animator.SetFloat("X", -rb.velocity.x);

        if (IsGrounded() && horizontalDirection != 0)
        {
            CheckForStep();
        }

    }
    private void AttachToFloor(bool isGrounded)
    {
        if (isGrounded && rb.velocity.y <.1f)
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
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, 0.15f, groundLayer | obstacleLayer);
        if (hit.collider)
        {
            currentGround = hit.transform;
            return true;
        }
        currentGround = null;
        return false;
    }

    private void CheckForStep()
    {
        RaycastHit2D hitFront = Physics2D.Raycast(feetPosition.position + new Vector3(horizontalDirection * stepCheckDistance, 0, 0), Vector2.right, stepHeight + 0.1f, groundLayer | obstacleLayer);

        if (hitFront && currentGround)
        {
            float yDifference = hitFront.transform.position.y + hitFront.transform.localScale.y / 2 - currentGround.transform.position.y - hitFront.transform.localScale.y / 2 + .05f;

            if (yDifference > 0 && yDifference <= stepHeight)
            {
                rb.position = new Vector2(rb.position.x, rb.position.y + yDifference);
            }
        }
    }

}

