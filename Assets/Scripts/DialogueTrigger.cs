using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string Name;
    public Dialogue[] dialogues;
    public int currentDialogueIndex = 0;
    private void Start()
    {

    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(this);
    }
}
