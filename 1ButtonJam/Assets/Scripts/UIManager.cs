using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Buttons Configuration")]
    public GameObject pauseMenu; // Optional: Reference to the pause menu, if you have one

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Ensure the pause menu is hidden at the start
        }
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    // Start a new game by loading the "Intro" scene
    public void NewGame()
    {
        Debug.Log("Starting New Game...");
        SceneManager.LoadScene("Intro");
    }

    // Resume the current game by reloading the active scene
    public void ResumeGame()
    {
        Debug.Log("Resuming Current Scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: Toggle the pause menu (if you need it)
    public void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
