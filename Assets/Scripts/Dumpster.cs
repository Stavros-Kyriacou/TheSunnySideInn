using UnityEngine;
using UnityEngine.Events;

public class Dumpster : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
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
        OnInteract.Invoke();
    }
}
