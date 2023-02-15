using System;
using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Carriage")]
    [SerializeField] private Transform carriage;
    [SerializeField] private float carriageMovementDuration;

    [Header("Doors")]
    [SerializeField] private Transform bottomLeftDoor;
    [SerializeField] private Transform bottomRightDoor;
    [SerializeField] private Transform topLeftDoor;
    [SerializeField] private Transform topRightDoor;

    [SerializeField] private float doorMovementDuration;

    [Header("Positions")]
    [SerializeField] private Transform bottomDoorClosedPos;
    [SerializeField] private Transform bottomLeftDoorOpenPos;
    [SerializeField] private Transform bottomRightDoorOpenPos;
    [SerializeField] private Transform topDoorClosedPos;
    [SerializeField] private Transform topLeftDoorOpenPos;
    [SerializeField] private Transform topRightDoorOpenPos;
    [SerializeField] private Transform carriageBottomPosition;
    [SerializeField] private Transform carriageTopPosition;

    [Header("Up/Down Lights")]
    [SerializeField] private Material lightOnMat;
    [SerializeField] private Material lightOffMat;
    [SerializeField] private MeshRenderer[] downArrows;
    [SerializeField] private MeshRenderer[] upArrows;


    Coroutine runningCoroutine;
    bool elevatorMoving = false;
    private void Start()
    {
        if (CarriageAtBottom())
        {
            ChangeLights(ElevatorLights.Down);
        }
        else
        {
            ChangeLights(ElevatorLights.Up);
        }
    }
    bool BottomDoorsClosed()
    {
        if (bottomLeftDoor.transform.position == bottomDoorClosedPos.transform.position
        && bottomRightDoor.transform.position == bottomDoorClosedPos.transform.position)
        {
            return true;
        }
        else if (bottomLeftDoor.transform.position == bottomLeftDoorOpenPos.transform.position
        && bottomRightDoor.transform.position == bottomRightDoorOpenPos.transform.position)
        {
            return false;
        }
        return false;
    }
    bool TopDoorsClosed()
    {
        if (topLeftDoor.transform.position == topDoorClosedPos.transform.position
        && topRightDoor.transform.position == topDoorClosedPos.transform.position)
        {
            return true;
        }
        else if (topLeftDoor.transform.position == topLeftDoorOpenPos.transform.position
        && topRightDoor.transform.position == topRightDoorOpenPos.transform.position)
        {
            return false;
        }
        return false;
    }
    bool CarriageAtBottom()
    {
        if (carriage.transform.position == carriageBottomPosition.transform.position)
        {
            return true;
        }
        else if (carriage.transform.position == carriageTopPosition.transform.position)
        {
            return false;
        }
        return false;
    }
    public void LowerButtonPress()
    {
        if (runningCoroutine != null) return;

        if (CarriageAtBottom() && BottomDoorsClosed())
        {
            runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Bottom));
        }
        else if (!CarriageAtBottom() && BottomDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageDown());
        }
        else if (CarriageAtBottom() && !BottomDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.Bottom));
        }
        else if (!CarriageAtBottom() && !BottomDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageDown());
        }
        ChangeLights(ElevatorLights.Down);
    }
    public void TopButtonPress()
    {
        if (runningCoroutine != null) return;

        if (!CarriageAtBottom() && TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Top));
        }
        else if (CarriageAtBottom() && TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageUp());
        }
        else if (!CarriageAtBottom() && !TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.Top));
        }
        else if (CarriageAtBottom() && !BottomDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageUp());
        }
        ChangeLights(ElevatorLights.Up);
    }
    public void CarriageButtonPress()
    {
        if (runningCoroutine != null || elevatorMoving) return;

        elevatorMoving = true;

        if (CarriageAtBottom())
        {
            runningCoroutine = StartCoroutine(BringCarriageUp());
            ChangeLights(ElevatorLights.Up);
        }
        else
        {
            runningCoroutine = StartCoroutine(BringCarriageDown());
            ChangeLights(ElevatorLights.Down);
        }
    }
    IEnumerator OpenDoors(ElevatorDoor door)
    {
        switch (door)
        {
            case ElevatorDoor.All:
                {
                    Vector3 topLeftStartPos = topLeftDoor.transform.position;
                    Vector3 bottomLeftStartPos = bottomLeftDoor.transform.position;
                    Vector3 topRightStartPos = topRightDoor.transform.position;
                    Vector3 bottomRightStartPos = bottomRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(topLeftStartPos, topLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(topRightStartPos, topRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        bottomLeftDoor.transform.position = Vector3.Slerp(bottomLeftStartPos, bottomLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        bottomRightDoor.transform.position = Vector3.Slerp(bottomRightStartPos, bottomRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topLeftDoorOpenPos.transform.position;
                    topRightDoor.transform.position = topRightDoorOpenPos.transform.position;
                    bottomLeftDoor.transform.position = bottomLeftDoorOpenPos.transform.position;
                    bottomRightDoor.transform.position = bottomRightDoorOpenPos.transform.position;

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

            case ElevatorDoor.Bottom:
                {
                    Vector3 leftStartPos = bottomLeftDoor.transform.position;
                    Vector3 rightStartPos = bottomRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        bottomLeftDoor.transform.position = Vector3.Slerp(leftStartPos, bottomLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        bottomRightDoor.transform.position = Vector3.Slerp(rightStartPos, bottomRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    bottomLeftDoor.transform.position = bottomLeftDoorOpenPos.transform.position;
                    bottomRightDoor.transform.position = bottomRightDoorOpenPos.transform.position;

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
    IEnumerator CloseDoors(ElevatorDoor door)
    {
        switch (door)
        {
            case ElevatorDoor.All:
                {
                    Vector3 topLeftStartPos = topLeftDoor.transform.position;
                    Vector3 bottomLeftStartPos = bottomLeftDoor.transform.position;
                    Vector3 topRightStartPos = topRightDoor.transform.position;
                    Vector3 bottomRightStartPos = bottomRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(topLeftStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(topRightStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        bottomLeftDoor.transform.position = Vector3.Slerp(bottomLeftStartPos, bottomDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        bottomRightDoor.transform.position = Vector3.Slerp(bottomRightStartPos, bottomDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topDoorClosedPos.transform.position;
                    topRightDoor.transform.position = topDoorClosedPos.transform.position;
                    bottomLeftDoor.transform.position = bottomDoorClosedPos.transform.position;
                    bottomRightDoor.transform.position = bottomDoorClosedPos.transform.position;

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

            case ElevatorDoor.Bottom:
                {
                    Vector3 leftStartPos = bottomLeftDoor.transform.position;
                    Vector3 rightStartPos = bottomRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        bottomLeftDoor.transform.position = Vector3.Slerp(leftStartPos, bottomDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        bottomRightDoor.transform.position = Vector3.Slerp(rightStartPos, bottomDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    bottomLeftDoor.transform.position = bottomDoorClosedPos.transform.position;
                    bottomRightDoor.transform.position = bottomDoorClosedPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                }
                break;
            default:
                runningCoroutine = null;
                break;
        }
    }
    IEnumerator BringCarriageUp()
    {
        if (!BottomDoorsClosed() || !TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.All));
            yield return new WaitForSeconds(doorMovementDuration + 1f);
        }
        runningCoroutine = StartCoroutine(CarriageUp());
        yield return new WaitForSeconds(carriageMovementDuration + 1f);
        runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Top));
    }
    IEnumerator BringCarriageDown()
    {
        if (!BottomDoorsClosed() || !TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.All));
            yield return new WaitForSeconds(doorMovementDuration + 1f);
        }
        runningCoroutine = StartCoroutine(CarriageDown());
        yield return new WaitForSeconds(carriageMovementDuration + 1f);
        runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Bottom));
    }
    IEnumerator CarriageUp()
    {
        Vector3 startPos = carriage.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < carriageMovementDuration)
        {
            carriage.transform.position = Vector3.Lerp(startPos, carriageTopPosition.transform.position, elapsedTime / carriageMovementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Make sure the position is exact
        carriage.transform.position = carriageTopPosition.transform.position;

        //Movement complete
        runningCoroutine = null;
    }
    IEnumerator CarriageDown()
    {
        Vector3 startPos = carriage.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < carriageMovementDuration)
        {
            carriage.transform.position = Vector3.Lerp(startPos, carriageBottomPosition.transform.position, elapsedTime / carriageMovementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Make sure the position is exact
        carriage.transform.position = carriageBottomPosition.transform.position;

        //Movement complete
        runningCoroutine = null;
    }
    private void ChangeLights(ElevatorLights direction)
    {
        switch (direction)
        {
            case ElevatorLights.Up:
                for (int i = 0; i < upArrows.Length; i++)
                {
                    upArrows[i].material = lightOnMat;
                    downArrows[i].material = lightOffMat;
                }
                break;
            case ElevatorLights.Down:
                for (int i = 0; i < downArrows.Length; i++)
                {
                    downArrows[i].material = lightOnMat;
                    upArrows[i].material = lightOffMat;
                }
                break;
            default:
                break;
        }
    }
}
public enum ElevatorDoor
{
    Bottom,
    Top,
    All
}
public enum ElevatorLights
{
    Up,
    Down
}