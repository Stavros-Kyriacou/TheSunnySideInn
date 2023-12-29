using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Dumpster : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    [SerializeField] private float playerRotateTime;
    [SerializeField] private Transform directionTrigger;
    [SerializeField] private DirectionDetection directionDetection;
    [SerializeField] private NPCMovement samanthaDumpster;
    private Animator animator;
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        IsInteractable = isInteractable;
        InteractMessage = interactMessage;
    }
    public void Interact()
    {
        OnInteract.Invoke();
    }
    public void PlayerLeftBounds()
    {
        StartCoroutine(RotatePlayerToDumpster());
    }
    private IEnumerator RotatePlayerToDumpster()
    {
        Player.Instance.EnableMovement(false);

        float playerXPos = Player.Instance.transform.position.x;
        float yRot = Player.Instance.cameraController.Y_Rotation;
        float convertedYRot = 0;

        if (yRot <= 0)
        {
            convertedYRot = (yRot % 360) + 360;
        }
        else if (yRot >= 360)
        {
            convertedYRot = yRot % 360;
        }
        else
        {
            convertedYRot = yRot;
        }

        Player.Instance.cameraController.RotateCamera(Player.Instance.cameraController.X_Rotation, convertedYRot);
        yield return new WaitForEndOfFrame();

        directionTrigger.gameObject.SetActive(true);

        if (playerXPos > 0)
        {
            Player.Instance.cameraController.RotateCameraOverTime(0, 270, playerRotateTime, true);
            directionTrigger.transform.localPosition = new Vector3(2.25f, 0, 0);
        }
        else
        {
            Player.Instance.cameraController.RotateCameraOverTime(0, 90, playerRotateTime, true);
            directionTrigger.transform.localPosition = new Vector3(-2.25f, 0, 0);
        }

        yield return new WaitForSeconds(playerRotateTime);

        //Enable direction detection
        directionDetection.enabled = true;

        //Close dumpster lid
        animator.SetTrigger("OnClose");
    }
    public void DumpsterJumpScare()
    {
        samanthaDumpster.gameObject.SetActive(true);
        Vector3 spawnPos = Player.Instance.transform.position + (Player.Instance.cameraController.playerCam.transform.forward * 2);
        samanthaDumpster.transform.position = new Vector3(spawnPos.x, 0, spawnPos.z);
        Vector3 desiredDirection = Player.Instance.transform.position - samanthaDumpster.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
        samanthaDumpster.transform.rotation = lookRotation;
        samanthaDumpster.PlayDumpterScareSequence();
        directionTrigger.gameObject.SetActive(false);
    }
}
