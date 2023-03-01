using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Queue<string> sentences;
    private DialogueTrigger currentDialogueTrigger;
    private bool dialogueActive = false;
    private bool typingSentence = false;
    private string currentSentence;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }
    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        if (dialogueTrigger == null) return;

        dialogueActive = true;
        currentDialogueTrigger = dialogueTrigger;

        Player.Instance.MovementEnabled = false;
        Player.Instance.InteractionEnabled = false;
        dialogueBox.SetActive(true);

        nameText.text = dialogueTrigger.name;

        var currentDialogue = dialogueTrigger.dialogues[dialogueTrigger.currentDialogueIndex];

        sentences.Clear();

        foreach (var sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        typingSentence = true;
        dialogueText.text = "";
        foreach (var letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        typingSentence = false;
    }
    private void SkipSentence()
    {
        StopAllCoroutines();
        typingSentence = false;
        dialogueText.text = currentSentence;
    }
    private void EndDialogue()
    {
        Player.Instance.MovementEnabled = true;
        Player.Instance.InteractionEnabled = true;

        if (currentDialogueTrigger.currentDialogueIndex < (1 - currentDialogueTrigger.currentDialogueIndex))
        {
            currentDialogueTrigger.currentDialogueIndex++;
        }
        dialogueActive = false;
        dialogueBox.SetActive(false);
        currentDialogueTrigger.OnDialogueComplete.Invoke();
    }
    private void Update()
    {
        //TODO: Create input action and dont use update method
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (typingSentence)
            {
                SkipSentence();
                return;
            }
            DisplayNextSentence();
        }
    }
    private void OnDisable()
    {
        Destroy(this);
    }
    private void OnDestroy()
    {
        Destroy(this);
    }
}

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
}