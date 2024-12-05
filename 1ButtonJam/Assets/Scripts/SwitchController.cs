using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Switch Settings")]
    public Sprite pressedSprite; // The sprite to show when the switch is pressed
    private SpriteRenderer spriteRenderer;

    [Header("Door Settings")]
    public Animator doorAnimator; // Animator component for the door
    public string doorOpenTrigger = "Open"; // Trigger name to play the door open animation
    public string doorOpenStateName = "DoorOpen"; // Name of the door open state in the Animator

    [Header("Persistent State")]
    public string switchID = "Switch1"; // Unique identifier for this switch
    public string playerPrefsKey = "SwitchState_"; // Key prefix for PlayerPrefs

    private bool isPressed = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the switch has been previously pressed
        if (PlayerPrefs.GetInt(playerPrefsKey + switchID, 0) == 1)
        {
            isPressed = true;
            spriteRenderer.sprite = pressedSprite;

            // Ensure the door remains open
            if (doorAnimator != null)
            {
                doorAnimator.Play(doorOpenStateName, 0, 1f); // Set the door to the fully open state
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            ActivateSwitch();

            // Save the state
            PlayerPrefs.SetInt(playerPrefsKey + switchID, 1);
            PlayerPrefs.Save();
        }
    }

    private void ActivateSwitch()
    {
        isPressed = true;

        // Change the switch sprite
        if (pressedSprite != null)
        {
            spriteRenderer.sprite = pressedSprite;
        }

        // Play the door open animation
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(doorOpenTrigger);
        }
    }
}
