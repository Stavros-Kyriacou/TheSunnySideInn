using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour, IInteractable
    {
        [Header("Interaction")]
        public UnityEvent OnInteract;
        [SerializeField] private string interactMessage;
        [SerializeField] private bool isInteractable;
        public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
        public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }

        public void Interact()
        {
            OnInteract.Invoke();
        }
    }
}