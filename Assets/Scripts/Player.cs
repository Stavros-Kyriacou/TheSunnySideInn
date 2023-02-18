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
