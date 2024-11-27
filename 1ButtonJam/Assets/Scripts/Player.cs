using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    public float glideGravityScale = 0.25f;
    public float normalGravityScale = 1f;
    public Rigidbody2D rb;

    private bool isGrounded = true;
    private bool isFacingRight = true;
    private float horizontalMove = 0f;
    private float lastPressTime = 0f;
    private int pressCount = 0;

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            HandlePress();
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (pressCount >= 2)
            {
                horizontalMove = isFacingRight ? moveSpeed : -moveSpeed; // Move forward after double press
            }

            if (pressCount >= 3)
            {
                horizontalMove = isFacingRight ? runSpeed : -runSpeed; // Run after triple press
            }
        }
        else
        {
            horizontalMove = 0f; // Stop moving when not holding
        }

        if (!isGrounded) // Glide when in air
        {
            rb.gravityScale = glideGravityScale;
            rb.AddForce(new Vector2(isFacingRight ? moveSpeed : -moveSpeed, 0), ForceMode2D.Force);
        }
        else
        {
            rb.gravityScale = normalGravityScale;
        }
    }

    private void HandlePress()
    {
        float timeSinceLastPress = Time.time - lastPressTime;

        if (timeSinceLastPress <= 0.3f) // Detect double or triple press
        {
            pressCount++;
        }
        else
        {
            pressCount = 1;
        }

        lastPressTime = Time.time;

        if (pressCount == 2 && isGrounded)
        {
            Jump();
        }
        else if (pressCount == 3)
        {
            Flip();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGrounded = true;
        }
    }
}
