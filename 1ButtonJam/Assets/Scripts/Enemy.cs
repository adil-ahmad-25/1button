using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 3;

    [Header("Patrol Settings")]
    public Transform pointA; // First point
    public Transform pointB; // Second point
    public float speed = 2f; // Movement speed

    private Transform currentTarget; // Current target point
    private bool facingRight = false; // Track if the enemy is facing right

    private void Start()
    {
        currentTarget = pointA; // Start moving towards point A
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        // Move towards the current target
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Check if the enemy has reached the current target
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            Flip(); // Flip the enemy's direction
            currentTarget = (currentTarget == pointA) ? pointB : pointA; // Switch target
        }
    }

    private void Flip()
    {
        facingRight = !facingRight; // Toggle the facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the enemy's scale on the X-axis
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1); // Decrease health by 1
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage; // Decrease health

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
