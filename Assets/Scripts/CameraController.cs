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
    public void RotateCameraOverTime(Transform target, float duration)
    {
        Debug.Log("rotate camera over time");
        Player.Instance.CameraEnabled = false;
        cameraHolder.transform.LookAt(target);
        Player.Instance.CameraEnabled = true;
        // StartCoroutine(RotateCameraOverTimeRoutine(target, duration));
    }
    public IEnumerator RotateCameraOverTimeRoutine(Transform target, float duration)
    {
        Player.Instance.CameraEnabled = false;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cameraHolder.transform.LookAt(target);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Player.Instance.CameraEnabled = true;
    }
}
