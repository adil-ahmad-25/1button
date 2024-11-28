using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 5f; // Force applied when jumping
    public float pressWindowTime = 0.5f; // Time window for double/triple press

    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator component
    private bool isMoving = false;
    private bool isFacingRight = true; // Track the player's facing direction
    private bool isGrounded = true; // Check if the player is on the ground

    private float lastPressTime = 0f; // Time of the last press
    private int pressCount = 0; // Count the number of presses within the time window

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
        }

        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    void Update()
    {
        // Check for input (Spacebar or Left Mouse Button)
        bool isPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (isPressed)
        {
            if (Time.time - lastPressTime <= pressWindowTime)
            {
                pressCount++;

                if (pressCount == 2 && isGrounded) // Double press: Jump
                {
                    Jump();
                    pressCount = 0; // Reset the press count
                }
                else if (pressCount == 3) // Triple press: Flip
                {
                    Flip();
                    pressCount = 0; // Reset the press count
                }
            }
            else
            {
                // Reset the press count if the time window has elapsed
                pressCount = 1;
            }

            lastPressTime = Time.time; // Update the last press time
        }

        // Check if the input is held down
        isMoving = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);

        // Update the animation state
        if (animator != null)
        {
            animator.SetBool("isRunning", isMoving && isGrounded);

            // Set jump and fall animations based on vertical velocity
            if (!isGrounded)
            {
                if (rb.velocity.y > 0)
                {
                    animator.SetBool("isJumping", true);
                    animator.SetBool("isFalling", false);
                }
                else if (rb.velocity.y < 0)
                {
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isFalling", true);
                }
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement if the player is moving
        if (isMoving)
        {
            float direction = isFacingRight ? 1 : -1; // Determine the direction based on facing
            rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
        }
    }

    void Jump()
    {
        // Apply jump force and set grounded to false
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        Debug.Log("Jumped!");
    }

    void Flip()
    {
        // Flip the direction the player is facing
        isFacingRight = !isFacingRight;

        // Flip the sprite's local scale (to visually flip the character)
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        Debug.Log("Flipped!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has landed on the ground
        if (collision.contacts.Length > 0 && collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
