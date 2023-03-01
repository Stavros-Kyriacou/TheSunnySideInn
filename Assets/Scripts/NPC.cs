using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private DialogueTrigger dialogueTrigger;
    [SerializeField] private Item playerRoomKey;
    public bool IsInteractable { get; set; }
    public string InteractMessage { get; set; }
    [SerializeField] private string interactMessage;

    private void Awake()
    {
        IsInteractable = true;
        InteractMessage = interactMessage;
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    public void Interact()
    {
        if (dialogueTrigger == null) return;

        dialogueTrigger.TriggerDialogue();
    }
    public void GivePlayerKey()
    {
        if (playerRoomKey == null) return;
        if (GameManager.Instance.Room_Key_Acquired) return;

        InventoryManager.Instance.AddItem(playerRoomKey);
        UIManager.Instance.DisplayNotifyText("Room key acquired");
    }
}
