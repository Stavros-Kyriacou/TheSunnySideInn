using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueComplete;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(this);
    }
}
