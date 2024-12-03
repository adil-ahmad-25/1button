using System.Collections;
using UnityEngine;

public class PlayerLadderClimb : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbSpeed = 3f; // Speed at which the player climbs the ladder
    public LayerMask ladderLayer; // Layer for the ladders

    private bool isInContactWithLadder = false; // Check if the player is near a ladder
    private bool isClimbing = false; // Check if the player is actively climbing
    private float lastClickTime = 0f; // Track the last mouse click time
    private float doubleClickThreshold = 0.3f; // Time threshold for double-click detection

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isInContactWithLadder)
        {
            HandleLadderInput();
        }

        if (isClimbing)
        {
            ClimbLadder();
        }
    }

    private void HandleLadderInput()
    {
        // Detect single or double left mouse clicks
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Double-click: Climb down
                isClimbing = true;
                StartCoroutine(ClimbDirection(-1)); // Negative direction for climbing down
            }
            else
            {
                // Single click: Climb up
                isClimbing = true;
                StartCoroutine(ClimbDirection(1)); // Positive direction for climbing up
            }

            lastClickTime = Time.time;
        }
    }

    private IEnumerator ClimbDirection(int direction)
    {
        while (isClimbing)
        {
            rb.velocity = new Vector2(0, climbSpeed * direction); // Set vertical velocity
            yield return null;
        }
    }

    private void ClimbLadder()
    {
        rb.gravityScale = 0; // Disable gravity while climbing

        if (Input.GetMouseButtonUp(0)) // Stop climbing when the left mouse button is released
        {
            StopClimbing();
        }
    }

    private void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = 10; // Restore gravity
        rb.velocity = Vector2.zero; // Stop vertical movement
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is touching a ladder
        if ((ladderLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isInContactWithLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player is leaving the ladder
        if ((ladderLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isInContactWithLadder = false;
            StopClimbing();
        }
    }
}
