using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBasedSceneLoader : MonoBehaviour
{
    [Header("Scene Loader Settings")]
    [Tooltip("Enter the name of the scene to load.")]
    public string sceneToLoad;

    [Tooltip("Tag for the player object.")]
    public string playerTag = "Player";

    [Header("Transition Settings")]
    [Tooltip("Animator for scene transitions.")]
    public Animator transitionAnimator;

    [Tooltip("Duration of the transition animation.")]
    public float transitionDuration = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger has the specified tag
        if (collision.CompareTag(playerTag))
        {
            StartCoroutine(LoadSceneWithTransition());
        }
    }

    /// <summary>
    /// Loads the scene with a transition animation.
    /// </summary>
    private IEnumerator LoadSceneWithTransition()
    {
        if (transitionAnimator != null)
        {
            // Play the transition animation
            transitionAnimator.SetTrigger("End");
        }

        // Wait for the animation to finish
        yield return new WaitForSeconds(transitionDuration);

        // Load the new scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name is empty! Please assign a scene name in the inspector.");
        }
    }
}
