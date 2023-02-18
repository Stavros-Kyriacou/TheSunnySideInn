using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private float playerHeight = 2f;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float moveSpeedMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;


    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;


    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float gravityMultiplier = 2f;


    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private PhysicMaterial physicMaterial;
    public bool isGrounded;
    private float groundDistance = 0.4f;


    private float horizontalMovement, verticalMovement;
    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;
    private Rigidbody rb;
    private RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (!Player.Instance.MovementEnabled) return;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }
    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveSpeedMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 0;
        }
        else if (isGrounded && OnSlope() && moveDirection == Vector3.zero)
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * moveSpeedMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 2;
        }
        else if (isGrounded && OnSlope() && moveDirection != Vector3.zero)
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * moveSpeedMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 0;
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveSpeedMultiplier * airMultiplier, ForceMode.Acceleration);
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 0;
        }
    }
}

// private FPS_Controller inputController;


//     [SerializeField] private Camera playerCamera;
//     private Vector2 mouseMovement;
//     private float xRotation, yRotation;
//     [SerializeField] private float cameraOffset;
//     [SerializeField] private float mouseSensitivity = 50;


//     private Rigidbody rigidBody;
//     private Vector2 movement = Vector2.zero;
//     private float speed = 0f;
//     [SerializeField] private float walkSpeed = 5000f;
//     [SerializeField] private float sprintSpeed = 6000f;

//     private bool isSprinting;

//     private void Awake()
//     {
//         inputController = new FPS_Controller();
//         rigidBody = GetComponent<Rigidbody>();

//         cameraOffset = playerCamera.transform.position.y - transform.position.y;
//         speed = walkSpeed;

//         inputController.Player_Map.SprintStart.performed += x => SprintPressed();
//         inputController.Player_Map.SprintEnd.performed += x => SprintReleased();
//     }

//     private void SprintReleased()
//     {
//         isSprinting = false;
//         speed = walkSpeed;
//     }

//     private void SprintPressed()
//     {
//         isSprinting = true;
//         speed = sprintSpeed;
//     }

//     private void Update()
//     {
//         mouseMovement = Mouse.current.delta.ReadValue();
//         xRotation += mouseMovement.y * -1 * mouseSensitivity * Time.deltaTime;
//         xRotation = Mathf.Clamp(xRotation, -90, 90);
//         yRotation += mouseMovement.x * mouseSensitivity * Time.deltaTime;
//         playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
//         rigidBody.MoveRotation(Quaternion.Euler(0, yRotation, 0));
//         playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraOffset, transform.position.z);


//         movement = inputController.Player_Map.Movement.ReadValue<Vector2>();
//         rigidBody.AddForce(transform.forward * movement.y * speed, ForceMode.Force);
//         rigidBody.AddForce(transform.right * movement.x * speed);
//     }
//     private void OnEnable() {
//         inputController.Enable();
//     }
//     private void OnDisable() {
//         inputController.Disable();
//     }