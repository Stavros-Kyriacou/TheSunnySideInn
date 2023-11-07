using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private bool startsLocked;
    [SerializeField] private bool requiresKey;
    [SerializeField] private string keyId;
    [SerializeField] private string doorLockedMessage;

    [Header("Audio")]
    [SerializeField] private Transform audioLocation;

    [Header("Events")]
    public UnityEvent OnUnlocked;
    public UnityEvent OnOpened;
    public UnityEvent OnClosed;

    [Header("Animation")]
    [HideInInspector] public bool isMoving;


    private Animator animator;
    private bool doorLocked;
    private bool doorOpen = false;

    public bool IsOpen { get { return doorOpen; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No animator found");
        }

        if (startsLocked || requiresKey)
        {
            doorLocked = true;
        }
        else
        {
            doorLocked = startsLocked;
        }
    }
    public void OpenDoor(int openSpeed)
    {
        if (requiresKey && doorLocked)
        {
            if (PlayerHasKey())
            {
                UnlockDoor();
                return;
            }
            else
            {
                UIManager.Instance.DisplayNotifyText(doorLockedMessage);
                return;
            }
        }

        if (doorLocked)
        {
            UIManager.Instance.DisplayNotifyText(doorLockedMessage);
            return;
        }

        if (isMoving) return;

        if (doorOpen)
        {
            animator.SetTrigger("OnClose");
        }
        else
        {
            switch (openSpeed)
            {
                case 0:
                    animator.SetTrigger("OnOpen");
                    break;
                case 1:
                    animator.SetTrigger("OnOpenSlow");
                    break;
                default:
                    animator.SetTrigger("OnOpen");

                    break;
            }
        }
        doorOpen = !doorOpen;
    }
    public void CloseDoor()
    {
        if (!doorOpen) return;

        animator.SetTrigger("OnClose");
        doorOpen = false;
    }

    private bool PlayerHasKey()
    {
        return InventoryManager.Instance.InventoryContainsKey(keyId);
    }
    public void UnlockDoor()
    {
        doorLocked = false;

        if (requiresKey)
        {
            UIManager.Instance.DisplayNotifyText("Unlocked");
        }
    }
    public void LockDoor()
    {
        doorLocked = true;
    }
    public void PlayAudioClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, audioLocation.position);
    }
    public void PlayHandleAnimation()
    {
        animator.SetTrigger("HandleAnimation");
    }
}
