using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LuggageArea : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteractComplete;
    public string InteractMessage { get; set; }
    public bool IsInteractable { get; set; }
    [SerializeField] private Item luggage;
    [SerializeField] private string interactMessage;
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        IsInteractable = true;
        InteractMessage = interactMessage;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Interact()
    {
        if (luggage == null) return;
        if (!GameManager.Instance.Luggage_Picked_Up) return;

        InventoryManager.Instance.RemoveItem(luggage);

        luggage.gameObject.SetActive(true);
        luggage.IsInteractable = false;
        luggage.transform.position = gameObject.transform.position;
        luggage.transform.SetParent(gameObject.transform);

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        IsInteractable = false;
        OnInteractComplete.Invoke();
    }
}
