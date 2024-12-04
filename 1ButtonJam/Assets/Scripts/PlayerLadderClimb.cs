using UnityEngine;

public class PlayerLadderClimb : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbSpeed = 3f; // Speed at which the player climbs the ladder
    public LayerMask ladderLayer; // Layer for the ladders

    private bool isInContactWithLadder = false; // Check if the player is near a ladder
    private bool isClimbing = false; // Check if the player is actively climbing

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isInContactWithLadder)
        {
            if (Input.GetMouseButton(0)) // Left mouse button held
            {
                StartClimbing();
            }
            else if (isClimbing)
            {
                StopClimbing();
            }
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;
        rb.gravityScale = 0; // Disable gravity while climbing
        rb.velocity = new Vector2(0, climbSpeed); // Move the player upward
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

            // Automatically start climbing if left mouse button is held
            if (Input.GetMouseButton(0))
            {
                StartClimbing();
            }
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
