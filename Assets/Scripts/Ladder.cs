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
    [SerializeField] private Elevator elevator;
    [SerializeField] private Carriage carriage;
    [SerializeField] private Ladder basementLadder;
    [SerializeField] private string interactMessage;
    [SerializeField] private bool isInteractable;
    private bool climbedLadder = false;
    private BoxCollider ladderCollider;
    private Rigidbody playerRigidBody;
    public bool IsInteractable { get { return isInteractable; } set { isInteractable = value; } }
    public string InteractMessage { get { return interactMessage; } set { interactMessage = value; } }
    public bool ClimbedLadder { get { return climbedLadder; } }
    private void Awake()
    {
        IsInteractable = isInteractable;
        InteractMessage = interactMessage;
    }
    void Start()
    {
        ladderCollider = GetComponent<BoxCollider>();
        playerRigidBody = Player.Instance.GetComponent<Rigidbody>();
    }
    public void Interact()
    {
        if (climbedLadder)
        {
            StartCoroutine("ClimbDownLadder");
        }
        else
        {
            StartCoroutine("ClimbUpLadder");
        }

        OnInteract.Invoke();
    }
    IEnumerator ClimbUpLadder()
    {
        ladderCollider.enabled = false;

        //Disable player movement
        Player.Instance.EnableMovement(false);
        Player.Instance.InteractionEnabled = false;

        //Calculate target position and animation time
        Vector3 startPos = Player.Instance.transform.position;
        Vector3 targetPos = new Vector3(ladderBottom.position.x, Player.Instance.transform.position.y, ladderBottom.position.z);
        float animationTime = (Vector3.Distance(startPos, targetPos) / 2.1f) * 0.8f;

        float elapsedTime = 0f;

        while (elapsedTime < animationTime)
        {
            //Move player to bottom of ladder
            playerRigidBody.MovePosition(Vector3.Lerp(startPos, targetPos, elapsedTime / animationTime));

            //Move camera
            cameraHolder.transform.LookAt(cameraTarget);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Fix camera position
        Vector3 direction = cameraTarget.position - Player.Instance.transform.position;

        //Rotate player camera to face ladder
        if (direction.z < 1)
        {
            Player.Instance.cameraController.RotateCamera(0, 90);
        }
        else
        {
            Player.Instance.cameraController.RotateCamera(0, 0);
        }

        //Climb ladder
        elapsedTime = 0f;
        startPos = Player.Instance.transform.position;
        float climbAnimationTime = 3f;

        while (elapsedTime < climbAnimationTime)
        {
            //Move player up ladder
            playerRigidBody.MovePosition(Vector3.Lerp(startPos, ladderTop.position, elapsedTime / climbAnimationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Freeze rigid body to stop player from falling
        playerRigidBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        ladderCollider.enabled = true;
        Player.Instance.InteractionEnabled = true;
        climbedLadder = true;
        yield return null;
    }
    IEnumerator ClimbDownLadder()
    {
        ladderCollider.enabled = false;
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        Player.Instance.InteractionEnabled = false;

        yield return new WaitForSeconds(2f);

        Player.Instance.InteractionEnabled = true;
        Player.Instance.EnableMovement(true);
        ladderCollider.enabled = true;
        climbedLadder = false;

        if (GameManager.Instance.Page_3_Acquired)
        {
            isInteractable = false;
            basementLadder.isInteractable = false;
            elevator.TeleportPlayerToBasement();
        }
    }
}
