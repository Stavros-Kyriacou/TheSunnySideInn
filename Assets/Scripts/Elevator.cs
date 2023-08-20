using System.Collections;
using UnityEngine;

public class Elevator : BaseElevator
{
    [SerializeField] private Ladder ladder;
    [SerializeField] private GameObject elevatorLights;
    [SerializeField] private GameObject elevatorLightsBasement;
    [SerializeField] private GameObject warningLight;
    [SerializeField] private GameObject warningLightBasement;
    [SerializeField] private ElevatorButton carriageButton;
    [SerializeField] private Carriage basementCarriage;
    protected override IEnumerator OpenDoors(ElevatorDoor door)
    {
        switch (door)
        {
            case ElevatorDoor.All:
                {
                    Vector3 topLeftStartPos = topLeftDoor.transform.position;
                    Vector3 bottomLeftStartPos = groundLeftDoor.transform.position;
                    Vector3 topRightStartPos = topRightDoor.transform.position;
                    Vector3 bottomRightStartPos = groundRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(topLeftStartPos, topLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(topRightStartPos, topRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        groundLeftDoor.transform.position = Vector3.Slerp(bottomLeftStartPos, groundLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        groundRightDoor.transform.position = Vector3.Slerp(bottomRightStartPos, groundRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topLeftDoorOpenPos.transform.position;
                    topRightDoor.transform.position = topRightDoorOpenPos.transform.position;
                    groundLeftDoor.transform.position = groundLeftDoorOpenPos.transform.position;
                    groundRightDoor.transform.position = groundRightDoorOpenPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                    elevatorMoving = false;
                }
                break;

            case ElevatorDoor.Top:
                {
                    Vector3 leftStartPos = topLeftDoor.transform.position;
                    Vector3 rightStartPos = topRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(leftStartPos, topLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(rightStartPos, topRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topLeftDoorOpenPos.transform.position;
                    topRightDoor.transform.position = topRightDoorOpenPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                    elevatorMoving = false;
                }
                break;

            case ElevatorDoor.Ground:
                {
                    Vector3 leftStartPos = groundLeftDoor.transform.position;
                    Vector3 rightStartPos = groundRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        groundLeftDoor.transform.position = Vector3.Slerp(leftStartPos, groundLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        groundRightDoor.transform.position = Vector3.Slerp(rightStartPos, groundRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    groundLeftDoor.transform.position = groundLeftDoorOpenPos.transform.position;
                    groundRightDoor.transform.position = groundRightDoorOpenPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                    elevatorMoving = false;
                }
                break;
            default:
                runningCoroutine = null;
                elevatorMoving = false;
                break;
        }
    }
    protected override IEnumerator CloseDoors(ElevatorDoor door)
    {
        switch (door)
        {
            case ElevatorDoor.All:
                {
                    Vector3 topLeftStartPos = topLeftDoor.transform.position;
                    Vector3 bottomLeftStartPos = groundLeftDoor.transform.position;
                    Vector3 topRightStartPos = topRightDoor.transform.position;
                    Vector3 bottomRightStartPos = groundRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(topLeftStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(topRightStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        groundLeftDoor.transform.position = Vector3.Slerp(bottomLeftStartPos, groundDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        groundRightDoor.transform.position = Vector3.Slerp(bottomRightStartPos, groundDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topDoorClosedPos.transform.position;
                    topRightDoor.transform.position = topDoorClosedPos.transform.position;
                    groundLeftDoor.transform.position = groundDoorClosedPos.transform.position;
                    groundRightDoor.transform.position = groundDoorClosedPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                }
                break;

            case ElevatorDoor.Top:
                {
                    Vector3 leftStartPos = topLeftDoor.transform.position;
                    Vector3 rightStartPos = topRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(leftStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(rightStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topDoorClosedPos.transform.position;
                    topRightDoor.transform.position = topDoorClosedPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                }
                break;

            case ElevatorDoor.Ground:
                {
                    Vector3 leftStartPos = groundLeftDoor.transform.position;
                    Vector3 rightStartPos = groundRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        groundLeftDoor.transform.position = Vector3.Slerp(leftStartPos, groundDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        groundRightDoor.transform.position = Vector3.Slerp(rightStartPos, groundDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    groundLeftDoor.transform.position = groundDoorClosedPos.transform.position;
                    groundRightDoor.transform.position = groundDoorClosedPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                }
                break;
            default:
                runningCoroutine = null;
                break;
        }
    }
    public void StartBasementSequence()
    {
        //Close elevator doors
        StartCoroutine(CloseDoors(ElevatorDoor.All));

        //Disable elevator lights
        elevatorLights.SetActive(false);
        elevatorLightsBasement.SetActive(false);

        //Disable elevator buttons
        carriageButton.IsInteractable = false;

        //Play warning light animation
        Animator warningAnimator = warningLight.GetComponent<Animator>();
        warningAnimator.SetTrigger("StartWarning");

        Animator warningAnimatorBasement = warningLightBasement.GetComponent<Animator>();
        warningAnimatorBasement.SetTrigger("StartWarning");
    }
    public void TeleportPlayerToBasement()
    {
        Vector3 playerLocalPosition = Player.Instance.transform.localPosition;
        Player.Instance.transform.SetParent(basementCarriage.transform);
        Player.Instance.transform.localPosition = playerLocalPosition;
    }
}