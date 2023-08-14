using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ladder : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
    [SerializeField] private Transform ladderTop;
    [SerializeField] private Transform ladderBottom;
    [SerializeField] private MoveCamera cameraHolder;
    [SerializeField] private Transform cameraTarget;
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
        isInteractable = false;
        interactMessage = "";
        StartCoroutine("ClimbLadder");
        OnInteract.Invoke();
    }
    IEnumerator ClimbLadder()
    {
        //Get player rigidbody
        Rigidbody playerRigidBody = Player.Instance.GetComponent<Rigidbody>();

        //Disable ladder collider
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        //Disable player movement
        Player.Instance.MovementEnabled = false;
        Player.Instance.CameraEnabled = false;

        //Calculate target position and animation time
        Vector3 startPos = Player.Instance.transform.position;
        Vector3 targetPos = new Vector3(ladderBottom.position.x, Player.Instance.transform.position.y, ladderBottom.position.z);
        float animationTime = (Vector3.Distance(startPos, targetPos) / 2.1f) * 0.8f;

        float elapsedTime = 0f;

        while (elapsedTime < animationTime)
        {
            //Move player
            playerRigidBody.MovePosition(Vector3.Lerp(startPos, targetPos, elapsedTime / animationTime));

            //Move camera
            cameraHolder.transform.LookAt(cameraTarget);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Fix camera position
        Vector3 direction = cameraTarget.position - Player.Instance.transform.position;
        Player.Instance.cameraController.X_Rotation = 0;
        if (direction.z < 1)
        {
            Player.Instance.cameraController.Y_Rotation = 180;
        }
        else
        {
            Player.Instance.cameraController.Y_Rotation = 0;
        }

        //Climb ladder
        elapsedTime = 0f;
        startPos = Player.Instance.transform.position;
        float climbAnimationTime = 3f;
        while (elapsedTime < climbAnimationTime)
        {
            playerRigidBody.MovePosition(Vector3.Lerp(startPos, ladderTop.position, elapsedTime / climbAnimationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        playerRigidBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        boxCollider.enabled = true;
        Player.Instance.MovementEnabled = true;
        Player.Instance.CameraEnabled = true;
        yield return null;
    }
}
