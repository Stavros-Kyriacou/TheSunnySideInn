using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform door;
    [SerializeField][Range(-90f, 90f)] private float doorRotationAmount;
    [SerializeField] private float doorMovementDuration;
    [SerializeField] private bool startsLocked;
    [SerializeField] private bool requiresKey;
    [SerializeField] private string keyId;
    [SerializeField] private string doorLockedMessage;
    public UnityEvent OnUnlocked;

    [Header("Double Door")]
    [SerializeField] private bool isDoubleDoor;
    [SerializeField] private Door otherDoor;

    public bool IsInteractable { get; set; }
    private bool doorMoving;
    private bool doorLocked;
    private bool doorOpen;
    private void Awake()
    {
        this.IsInteractable = true;
        doorMoving = false;
        doorOpen = false;
        if (startsLocked || requiresKey)
        {
            doorLocked = true;
        }
        else
        {
            doorLocked = startsLocked;
        }
    }
    public void Interact()
    {
        if (doorMoving) return;

        if (doorLocked)
        {
            UnlockDoor();
            return;
        }

        OpenDoor(false);
    }
    public void UnlockDoor()
    {
        if (requiresKey)
        {
            if (PlayerHasKey())
            {
                doorLocked = false;
                UIManager.Instance.DisplayNotifyText("Unlocked");
            }
            else
            {
                UIManager.Instance.DisplayNotifyText(doorLockedMessage);
            }
        }
        else
        {
            doorLocked = false;
        }
    }

    private bool PlayerHasKey()
    {
        return Inventory.Instance.InventoryContainsKey(keyId);
    }

    public void OpenDoor(bool fromOtherDoor)
    {
        if (fromOtherDoor)
        {
            StartCoroutine(RotateDoor());
            return;
        }
        
        if (isDoubleDoor && otherDoor != null)
        {

            otherDoor.OpenDoor(true);
        }
        StartCoroutine(RotateDoor());
    }
    private IEnumerator RotateDoor()
    {
        doorMoving = true;

        var startRotation = transform.localRotation;
        Quaternion endRotation;

        if (doorOpen)
        {
            endRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, doorRotationAmount, 0));
        }

        float elapsedTime = 0f;

        while (elapsedTime < doorMovementDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / doorMovementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRotation;

        doorMoving = false;
        doorOpen = !doorOpen;
    }
}
