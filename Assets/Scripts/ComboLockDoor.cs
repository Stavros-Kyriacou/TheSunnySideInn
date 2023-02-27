using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboLockDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private CombinationLock combinationLock;
    [SerializeField] private TextMeshProUGUI lockedText;
    [SerializeField] private float unlockedRotationAmount;
    [SerializeField] private float doorMovementDuration;

    public bool IsInteractable { get; set; }
    private void Awake()
    {
        IsInteractable = true;
        lockedText.enabled = false;
    }

    public void Interact()
    {
        if (combinationLock.IsLocked)
        {
            StopAllCoroutines();
            StartCoroutine(ShowWarningText());
        }
        else
        {
            StartCoroutine(UnlockDoor());
        }
    }

    IEnumerator UnlockDoor()
    {
        IsInteractable = false;
        var startRotation = transform.localRotation;
        var endRotation = Quaternion.Euler(new Vector3(0, unlockedRotationAmount, 0));
        float elapsedTime = 0f;

        while (elapsedTime < doorMovementDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / doorMovementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator ShowWarningText()
    {
        var startColour = new Color32(255, 255, 255, 255);
        var endColour = new Color32(255, 255, 255, 0);
        float elapsedTime = 0f;

        lockedText.enabled = true;

        while (elapsedTime < 1)
        {
            lockedText.color = Color32.Lerp(startColour, endColour, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lockedText.color = endColour;
    }
}
