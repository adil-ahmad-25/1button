using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 3;

    [Header("Patrol Settings")]
    public Transform pointA; // First patrol point
    public Transform pointB; // Second patrol point
    public float speed = 2f; // Patrol speed

    private Transform currentTarget; // Current patrol target
    private bool facingRight = false; // Track if the enemy is facing right

    private Animator animator; // Reference to the Animator component

    private void Start()
    {
        currentTarget = pointA; // Start moving towards point A
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        // Move towards the current patrol target
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Check if the enemy has reached the patrol target based on the x-coordinate only
        if (Mathf.Abs(transform.position.x - currentTarget.position.x) < 0.1f)
        {
            Flip(); // Flip direction
            currentTarget = (currentTarget == pointA) ? pointB : pointA; // Switch target
        }
    }

    private void Flip()
    {
        facingRight = !facingRight; // Toggle facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the enemy's scale on the X-axis
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if a bullet hits the enemy
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1); // Decrease health by 1
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with the enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Decrease the player's health by 1
            }
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage; // Decrease health

        // Play the hurt animation
        if (animator != null)
        {
            animator.SetTrigger("Hurt"); // Trigger the "Hurt" animation
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy destroyed!");
        Destroy(gameObject); // Destroy the enemy
    }
}
