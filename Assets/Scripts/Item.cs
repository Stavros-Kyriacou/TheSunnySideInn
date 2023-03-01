using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public UnityEvent OnPickup;
    [SerializeField] private string interactMessage;

    public bool IsInteractable { get; set; }
    public string InteractMessage { get; set; }

    private void Awake()
    {
        IsInteractable = true;
        InteractMessage = interactMessage;
    }
    public void Interact()
    {
        InventoryManager.Instance.AddItem(this);
        gameObject.SetActive(false);
        OnPickup.Invoke();
    }
}