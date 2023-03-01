using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public string Name;
    public Dialogue[] dialogues;
    public int currentDialogueIndex = 0;
    public UnityEvent OnDialogueComplete;
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(this);
    }
}
