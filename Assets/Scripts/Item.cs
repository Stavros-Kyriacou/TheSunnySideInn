using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public UnityEvent OnPickup;
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public string InteractMessage { get; set; }

    private void Awake()
    {
        IsInteractable = isInteractable;
        InteractMessage = interactMessage;
    }
    public void Interact()
    {
        InventoryManager.Instance.AddItem(this);
        gameObject.SetActive(false);
        OnPickup.Invoke();
    }
}