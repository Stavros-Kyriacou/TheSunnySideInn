using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    [Header("Door")]
    [SerializeField] private Door door;

    [Header("Interaction")]
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }

    public void Interact()
    {
        if (door == null) return;

        door.OpenDoor(0);
    }
}
