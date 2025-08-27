using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask pushableLayer;

    private bool isGrounded;
    private bool isPushable;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }

        if (Input.GetKey(KeyCode.C))
        {
            FindObjectOfType<RespawnManager>().KillPlayer();
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SFXManager.Instance.PlaySound(SFXManager.Instance.jumpSound, 0.2f);

        }

        isPushable = Physics2D.OverlapCircle(groundCheck.position, 0.1f, pushableLayer);

        if (Input.GetKey(KeyCode.UpArrow) && isPushable)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SFXManager.Instance.PlaySound(SFXManager.Instance.jumpSound, 0.2f);

        }

    }
}
