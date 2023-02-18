using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Queue<string> sentences;
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
    public void StartDialogue(Dialogue dialogue)
    {
        Player.Instance.MovementEnabled = false;
        Player.Instance.InteractionEnabled = false;
        dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
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
        dialogueBox.SetActive(false);
    }
    private void Update()
    {
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
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}