using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public Animator doorAnimator; // Animator for the door
    public string doorOpenTrigger = "Open"; // Trigger for the door opening animation
    public string doorOpenStateName = "DoorOpen"; // Name of the open animation state

    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(doorOpenTrigger); // Play the door open animation
        }
    }

    public void OpenDoorPersistent()
    {
        if (doorAnimator != null)
        {
            doorAnimator.Play(doorOpenStateName, 0, 1f); // Jump to the fully open state
        }

        // Optional: Disable collider if needed
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
