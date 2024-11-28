using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed of the player
    public float jumpForce = 5f; // Force applied when jumping
    public float pressWindowTime = 0.5f; // Time window for double/triple press

    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator component
    private bool isGrounded = true; // Check if the player is on the ground
    private bool isRunning = false; // Check if the player is running

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
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse is on the right or left of the player
        if (mousePosition.x > transform.position.x && transform.localScale.x < 0)
        {
            Flip(true); // Face right
        }
        else if (mousePosition.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip(false); // Face left
        }

        // Check if the mouse is at the same position as the player
        if (Mathf.Abs(mousePosition.x - transform.position.x) < 0.1f) // Small threshold for precision
        {
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }

        // Check for mouse input (double click for jump)
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (Time.time - lastPressTime <= pressWindowTime)
            {
                pressCount++;

                if (pressCount == 2 && isGrounded) // Double click: Jump
                {
                    Jump();
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

        // Update the animation state
        if (animator != null)
        {
            animator.SetBool("isRunning", isRunning && isGrounded); // Running only when moving and grounded
        }
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            // Move the player forward constantly
            float direction = transform.localScale.x > 0 ? 1 : -1; // Direction based on facing
            rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
        }
        else
        {
            // Stop the player if idle
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Jump()
    {
        // Apply jump force and set grounded to false
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        Debug.Log("Jumped!");
    }

    void Flip(bool faceRight)
    {
        // Flip the sprite based on the direction
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
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
