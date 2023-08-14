using UnityEngine;
using UnityEngine.Events;

public class Hatch : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
    [SerializeField] private Animator animator;
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
    private void Awake()
    {
        IsInteractable = isInteractable;
        InteractMessage = interactMessage;
    }
    public void Interact()
    {
        if (!GameManager.Instance.Screwdriver_Acquired)
        {
            Debug.Log("requires screwdriver");
            GameManager.Instance.Screwdriver_Acquired = true;
            return;
        }

        if (animator == null) return;

        animator.Play("Open_Hatch");
        isInteractable = false;
        interactMessage = "";
        var collider = GetComponent<BoxCollider>();
        collider.enabled = false;

        OnInteract.Invoke();
    }
}
