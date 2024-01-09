using UnityEngine;
using TMPro;
using Interfaces;

namespace Character.Components
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private Transform playerCam;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask mask;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private RectTransform crosshair;
        [SerializeField] private Vector3 regularCrosshairScale;
        [SerializeField] private Vector3 largeCrosshairScale;

        private void Start()
        {
            interactText.enabled = false;
        }
        private void Update()
        {
            if (!Player.Instance.InteractionEnabled)
            {
                interactText.enabled = false;
                return;
            }

            RaycastHit hit;
            IInteractable interactable = null;

            if (Physics.Raycast(playerCam.transform.position, playerCam.TransformDirection(Vector3.forward), out hit, rayDistance))
            {
                // Debug.DrawRay(playerCam.transform.position, playerCam.TransformDirection(Vector3.forward) * hit.distance, Color.blue);

                interactable = hit.transform.GetComponent<IInteractable>();

                if (interactable != null && interactable.IsInteractable)
                {
                    crosshair.localScale = largeCrosshairScale;
                    interactText.text = interactable.InteractMessage;
                    interactText.enabled = true;
                }
                else
                {
                    crosshair.localScale = regularCrosshairScale;
                    interactText.enabled = false;
                    interactable = null;
                }
            }
            else
            {
                // Debug.DrawRay(playerCam.transform.position, playerCam.TransformDirection(Vector3.forward) * rayDistance, Color.red);
                crosshair.localScale = regularCrosshairScale;
                interactText.enabled = false;
                interactable = null;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable == null) return;

                interactable.Interact();
            }
        }
    }
}