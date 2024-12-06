using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    [Header("UI Elements")]
    public GameObject heartsCanvas; // Drag and drop your Hearts Canvas prefab here
    public GameObject gameOverScreen; // Drag and drop your Game Over Screen prefab here

    public UnityEngine.UI.Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        // Ensure the Game Over screen is disabled at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        // Check if health has dropped to zero
        if (health <= 0)
        {
            TriggerGameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Ensure health doesn't go below 0
        if (health < 0)
        {
            health = 0;
        }
    }

    private void TriggerGameOver()
    {
        // Disable the hearts canvas
        if (heartsCanvas != null)
        {
            heartsCanvas.SetActive(false);
        }

        // Enable the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Optionally: Freeze the game by setting time scale to 0
        Time.timeScale = 0f;

        // Add any other game-over logic (e.g., show restart buttons, etc.)
    }

    // Handle collision with fire or enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            TriggerGameOver(); // Directly trigger Game Over
        }
    }
}
