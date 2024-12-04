using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Switch Settings")]
    public Sprite pressedSprite; // Sprite to show when the switch is pressed
    private SpriteRenderer spriteRenderer;

    [Header("Door Settings")]
    public DoorController doorController; // Reference to the DoorController for the door

    [Header("Persistent State")]
    public string switchID = "Switch1"; // Unique identifier for the switch
    public string playerPrefsKey = "SwitchState_"; // Key prefix for PlayerPrefs

    private bool isPressed = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check the saved state
        if (PlayerPrefs.GetInt(playerPrefsKey + switchID, 0) == 1)
        {
            // Set the switch as already pressed
            isPressed = true;
            spriteRenderer.sprite = pressedSprite;

            // Ensure the door is open
            if (doorController != null)
            {
                doorController.OpenDoorPersistent();
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

        // Immediately play the door open animation
        if (doorController != null)
        {
            doorController.OpenDoor();
        }
    }
}
