using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public CameraState CameraState
    {
        get
        {
            return cameraState;
        }
        set
        {
            cameraState = value;
        }
    }
    private CameraState cameraState;
    [SerializeField] private Transform playerCameraPosition;
    [SerializeField] private Transform doorUnlockPosition;
    private void Awake()
    {
        cameraState = CameraState.PLAYER;
    }
    private void Update()
    {
        switch (cameraState)
        {
            case CameraState.PLAYER:
                transform.position = playerCameraPosition.position;
                break;
            case CameraState.UNLOCK_DOOR:
                transform.position = doorUnlockPosition.position;
                break;
            default:
                transform.position = playerCameraPosition.position;
                break;
        }
    }
}

public enum CameraState
{
    PLAYER,
    UNLOCK_DOOR
}