using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboLockDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private CombinationLock combinationLock;
    [SerializeField] private float unlockedRotationAmount;
    [SerializeField] private float doorMovementDuration;
    public bool IsInteractable { get; set; }
    private void Awake()
    {
        IsInteractable = true;
    }

    public void Interact()
    {
        if (combinationLock.IsLocked)
        {
            UIManager.Instance.DisplayNotifyText("Door is locked");
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
}
