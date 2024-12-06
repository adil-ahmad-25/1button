using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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
}
