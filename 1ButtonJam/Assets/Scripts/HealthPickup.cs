using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Health Settings")]
    public int healthRestoreAmount = 5; // Amount to restore

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerHealth component
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Restore the player's health
                playerHealth.health = Mathf.Min(playerHealth.numOfHearts, healthRestoreAmount);

                // Destroy the health pickup object
                Destroy(gameObject);
            }
        }
    }
}
