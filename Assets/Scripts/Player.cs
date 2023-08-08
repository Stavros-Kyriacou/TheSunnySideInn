using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private CameraController cameraController;
    public bool MovementEnabled
    {
        set
        {
            movementEnabled = value;
        }
        get
        {
            return movementEnabled;
        }
    }
    public bool InteractionEnabled
    {
        set
        {
            interactionEnabled = value;
        }
        get
        {
            return interactionEnabled;
        }
    }
    private bool movementEnabled = true;
    private bool interactionEnabled = true;
    private void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = GetComponent<CameraController>();
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

        yield return new WaitForSeconds(duration);

        playerMovement.MoveSpeed = movementSpeed;
        cameraController.X_Sensitivity = xSens;
        cameraController.Y_Sensitivity = ySens;

        yield return null;
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