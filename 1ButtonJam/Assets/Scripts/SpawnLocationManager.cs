using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnLocationManager : MonoBehaviour
{
    [Header("Spawn Points Configuration")]
    [Tooltip("Map scene names to spawn points.")]
    public SceneSpawnPoint[] spawnPoints;

    private void Start()
    {
        // Get the name of the previous scene
        string previousScene = PlayerPrefs.GetString("LastScene", "");

        // Find the spawn point for the previous scene
        foreach (SceneSpawnPoint point in spawnPoints)
        {
            if (point.sceneName == previousScene)
            {
                // Set the player's position to the corresponding spawn point
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                if (point.spawnTransform != null)
                {
                    player.position = point.spawnTransform.position;
                }
                else
                {
                    Debug.LogWarning($"SpawnTransform not set for {point.sceneName}");
                }

                // Exit the loop as we've found the spawn location
                break;
            }
        }
    }

    private void OnDestroy()
    {
        // Save the current scene name before leaving
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class SceneSpawnPoint
{
    public string sceneName; // Name of the scene you are coming from
    public Transform spawnTransform; // Transform to use as the spawn point
}
