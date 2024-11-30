using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1); // Decrease health by 1
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage; // Decrease health

        if (health <= 0)
        {
            Die(); 
            // Call the Die method when health reaches 0
        }
    }

    void Die()
    {
        Debug.Log("Enemy destroyed!");
        Destroy(gameObject); // Destroy the enemy 
    }
}
