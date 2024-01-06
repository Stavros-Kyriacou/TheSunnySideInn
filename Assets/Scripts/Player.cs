using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private Rigidbody playerRigidbody;
    public CameraController cameraController;
    [SerializeField] private CapsuleCollider playerCollider;
    public bool ColliderEnabled { get { return playerCollider.enabled; } set { playerCollider.enabled = value; } }
    public bool MovementEnabled { get { return movementEnabled; } set { movementEnabled = value; } }
    public bool CameraEnabled { get { return cameraEnabled; } set { cameraEnabled = value; } }
    public bool InteractionEnabled { get { return interactionEnabled; } set { interactionEnabled = value; } }
    public Rigidbody RigidBody { get { return playerRigidbody;} private set { } }
    private bool movementEnabled = true;
    private bool cameraEnabled = true;
    private bool interactionEnabled = true;
    private void Awake()
    {
        Instance = this;
        ColliderEnabled = true;
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponent<CameraController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    public void MovePlayer(Vector3 startPosition, Vector3 endPosition, float movementDuration, bool cameraEnabled)
    {
        StartCoroutine(MovePlayerOverTime(startPosition, endPosition, movementDuration, cameraEnabled));
    }
    private IEnumerator MovePlayerOverTime(Vector3 startPosition, Vector3 endPosition, float movementDuration, bool cameraEnabled)
    {
        EnableMovement(false);
        InteractionEnabled = false;
        CameraEnabled = cameraEnabled;
        playerRigidbody.isKinematic = true;

        float elapsedTime = 0;
        while (elapsedTime < movementDuration)
        {
            playerRigidbody.MovePosition(Vector3.Lerp(startPosition, endPosition, elapsedTime / movementDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerRigidbody.transform.position = endPosition;

        EnableMovement(true);
        InteractionEnabled = true;
        CameraEnabled = true;
        playerRigidbody.isKinematic = false;
        yield return null;
    }
    public void SlowMovement(float duration)
    {
        StartCoroutine(SlowMovementRoutine(duration));
    }
    public IEnumerator SlowMovementRoutine(float duration)
    {
        float movementSpeed = playerMovement.MoveSpeed;
        float xSens = cameraController.X_Sensitivity;
        float ySens = cameraController.Y_Sensitivity;

        playerMovement.MoveSpeed = 0.8f;
        cameraController.X_Sensitivity = 10;
        cameraController.Y_Sensitivity = 10;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cameraController.X_Sensitivity = Mathf.Lerp(10, xSens, elapsedTime / duration);
            cameraController.Y_Sensitivity = Mathf.Lerp(10, ySens, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerMovement.MoveSpeed = movementSpeed;
        cameraController.X_Sensitivity = xSens;
        cameraController.Y_Sensitivity = ySens;

        yield return null;
    }
    public void EnableMovement(bool enabled)
    {
        if (enabled)
        {
            MovementEnabled = true;
        }
        else
        {
            MovementEnabled = false;
            playerMovement.MoveDirection = Vector3.zero;
        }
    }
    private void OnDisable()
    {
        Destroy(this);
    }
    private void OnDestroy()
    {
        Destroy(this);
    }
}