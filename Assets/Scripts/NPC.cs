using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private DialogueTrigger dialogueTrigger;

    public bool IsInteractable { get; set; }

    private void Awake()
    {
        IsInteractable = true;
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    public void Interact()
    {
        if (dialogueTrigger == null) return;

        dialogueTrigger.TriggerDialogue();
    }
}
