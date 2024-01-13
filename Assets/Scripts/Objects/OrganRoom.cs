using System.Collections;
using System.Collections.Generic;
using Character.Components;
using Managers;
using Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class OrganRoom : MonoBehaviour
{
    [SerializeField] private List<Vent> vents;
    [SerializeField] private float dialogueStartDelay;
    [SerializeField] private float slowMovementDelay;
    [SerializeField] private float fadeToBlackDelay;
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    public void BeginEndingSequence()
    {
        StartCoroutine(EndingRoutine());
    }
    private IEnumerator EndingRoutine()
    {
        yield return new WaitForSeconds(dialogueStartDelay);
        dialogueTrigger.TriggerDialogue();
        yield return null;
    }
    public void EndingDialogueComplete()
    {
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(slowMovementDelay);
        Player.Instance.SlowMovement(20f);
        yield return new WaitForSeconds(fadeToBlackDelay);
        UIManager.Instance.FadeToBlack(true);
        yield return null;
    }
    public void OpenVents()
    {
        for (int i = 0; i < vents.Count; i++)
        {
            vents[i].PlayParticles();
        }
    }
}