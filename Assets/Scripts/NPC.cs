using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private DialogueTrigger dialogueTrigger;
    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    public void Interact()
    {
        if (dialogueTrigger == null) return;

        dialogueTrigger.TriggerDialogue();
    }
}
