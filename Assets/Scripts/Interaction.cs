using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TextMeshProUGUI interactText;

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

            if (interactable != null)
            {
                interactText.enabled = true;
            }
            else
            {
                interactText.enabled = false;
                interactable = null;
            }
        }
        else
        {
            // Debug.DrawRay(playerCam.transform.position, playerCam.TransformDirection(Vector3.forward) * rayDistance, Color.red);
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
