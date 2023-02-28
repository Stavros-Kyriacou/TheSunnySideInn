using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public UnityEvent OnPickup;

    public bool IsInteractable { get; set; }
    private void Awake()
    {
        IsInteractable = true;
    }
    public void Interact()
    {
        InventoryManager.Instance.AddItem(this);
        gameObject.SetActive(false);
        OnPickup.Invoke();
    }
}