using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;
    private float mouseX;
    private float mouseY;
    private float sensMultiplier = 0.01f;
    private float xRotation;
    private float yRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (!Player.Instance.MovementEnabled) return;
        
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
}
