using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource; // Drag your AudioSource here in the inspector

    private static MusicManager instance; // Singleton instance

    private void Awake()
    {
        // Ensure only one instance of the MusicManager exists
        if (instance == null)
        {
            instance = this; // Assign the current instance
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate MusicManager
        }

        // Ensure the audio source is playing if not already
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
