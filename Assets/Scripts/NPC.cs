using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private DialogueTrigger dialogueTrigger;

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
}
