using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Triggers;
using Character.Components;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Queue<Sentence> sentences;
        private DialogueTrigger currentDialogueTrigger;
        private bool dialogueActive = false;
        private bool typingSentence = false;
        private Sentence currentSentence;
        private string currentSentenceString;
        private string currentSpeakerString;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            sentences = new Queue<Sentence>();
            dialogueBox.SetActive(false);
        }
        public void StartDialogue(DialogueTrigger dialogueTrigger)
        {
            if (dialogueTrigger == null) return;

            dialogueActive = true;
            currentDialogueTrigger = dialogueTrigger;
            currentDialogueTrigger.OnDialogueStart.Invoke();

            Player.Instance.MovementEnabled = false;
            Player.Instance.CameraEnabled = false;
            Player.Instance.InteractionEnabled = false;
            dialogueBox.SetActive(true);

            // nameText.text = dialogueTrigger.name;

            var currentDialogue = dialogueTrigger.dialogue;


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
        IEnumerator TypeSentence(Sentence sentence)
        {
            typingSentence = true;
            dialogueText.text = "";
            nameText.text = currentSentence.speaker;
            foreach (var letter in sentence.sentence)
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
            dialogueText.text = currentSentence.sentence;
        }
        private void EndDialogue()
        {
            Player.Instance.MovementEnabled = true;
            Player.Instance.CameraEnabled = true;
            Player.Instance.InteractionEnabled = true;

            // if (currentDialogueTrigger.currentDialogueIndex < (1 - currentDialogueTrigger.currentDialogueIndex))
            // {
            //     currentDialogueTrigger.currentDialogueIndex++;
            // }
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
        public Sentence[] sentences;
    }
    [System.Serializable]
    public class Sentence
    {
        public string speaker;
        [TextArea(3, 10)] public string sentence;
    }
}