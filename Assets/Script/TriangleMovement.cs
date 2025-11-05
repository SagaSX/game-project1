using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask pushableLayer;
    public LayerMask breakableWallLayer;

    private bool isGrounded;
    private bool isPushable;
    private bool isDashing = false;
    private bool canAirDash = true;
    private float dashTime;
    private Vector2 dashDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isPushable = Physics2D.OverlapCircle(groundCheck.position, 0.1f, pushableLayer);

        // Reset air dash
        if (isGrounded && !isDashing)
        {
            canAirDash = true;
        }

        if (!isDashing)
        {
            HandleMovement();
            HandleJump();
            HandleDash();
        }
        else
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            FindObjectOfType<RespawnManager>().KillPlayer();
        } 
    }

    void HandleMovement()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            moveInput = 1f;

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SFXManager.Instance.PlaySound(SFXManager.Instance.jumpSound, 0.2f);

        }

        if (Input.GetKey(KeyCode.UpArrow) && isPushable)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SFXManager.Instance.PlaySound(SFXManager.Instance.jumpSound, 0.2f);

        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool hasDirection = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

            if ((isGrounded || canAirDash) && hasDirection)
            {
                if (!isGrounded)
                    canAirDash = false;

                isDashing = true;
                dashTime = dashDuration;

                // Determine direction
                if (Input.GetKey(KeyCode.LeftArrow))
                    dashDirection = Vector2.left;
                else if (Input.GetKey(KeyCode.RightArrow))
                    dashDirection = Vector2.right;

                Debug.Log("Dashing with velocity: " + dashDirection * dashSpeed);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing && ((1 << collision.gameObject.layer) & breakableWallLayer) != 0)
        {
            Destroy(collision.gameObject);
            Debug.Log("Broke a wall!");

            if (SFXManager.Instance != null)
            {
                SFXManager.Instance.PlaySound(SFXManager.Instance.explosionSound, 1f);
            }
        }
    }


}
    
