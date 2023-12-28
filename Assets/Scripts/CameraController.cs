using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;
    [SerializeField] private MoveCamera cameraHolder;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;
    private float mouseX;
    private float mouseY;
    private float sensMultiplier = 0.01f;
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    public float X_Rotation { get { return xRotation; } private set { } }
    public float Y_Rotation { get { return yRotation; } private set { } }
    public float X_Sensitivity { get { return xSensitivity; } set { xSensitivity = value; } }
    public float Y_Sensitivity { get { return ySensitivity; } set { ySensitivity = value; } }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (!Player.Instance.CameraEnabled) return;

        MyInput();

        playerCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * xSensitivity * sensMultiplier;
        xRotation -= mouseY * ySensitivity * sensMultiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
    public void RotateCamera(float xRotation, float yRotation)
    {
        this.xRotation = xRotation;
        this.yRotation = yRotation;
        playerCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    public void RotateCameraOverTime(float endXRotation, float endYRotation, float duration, bool enableCameraMovementEnd)
    {
        StartCoroutine(RotateCameraOverTimeRoutine(endXRotation, endYRotation, duration, enableCameraMovementEnd));
    }
    private IEnumerator RotateCameraOverTimeRoutine(float endXRotation, float endYRotation, float duration, bool enableCameraMovementEnd)
    {
        Player.Instance.CameraEnabled = false;
        float elapsedTime = 0f;
        float startXRot = X_Rotation;
        float startYRot = Y_Rotation;
        float currentXRot = 0;
        float currentYRot = 0;

        while (elapsedTime < duration)
        {
            currentXRot = Mathf.Lerp(startXRot, endXRotation, elapsedTime / duration);
            currentYRot = Mathf.Lerp(startYRot, endYRotation, elapsedTime / duration);
            RotateCamera(currentXRot, currentYRot);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RotateCamera(endXRotation, endYRotation);
        Player.Instance.CameraEnabled = enableCameraMovementEnd;
    }
}
