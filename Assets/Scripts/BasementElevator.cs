using System;
using System.Collections;
using UnityEngine;

public class BasementElevator : BaseElevator
{
    [Header("Basement")]
    [SerializeField] private Transform basementLeftDoor;
    [SerializeField] private Transform basementRightDoor;
    [SerializeField] private Transform basementCarriagePos;
    [SerializeField] private Transform basementDoorClosedPos;
    [SerializeField] private Transform basementLeftDoorOpenPos;
    [SerializeField] private Transform basementRightDoorOpenPos;
    [SerializeField] private float basementMovementDuration;
    [SerializeField] private Light[] carriageLights;

    protected bool BasementDoorsClosed()
    {
        if (basementLeftDoor.transform.position == basementDoorClosedPos.transform.position
        && basementRightDoor.transform.position == basementDoorClosedPos.transform.position)
        {
            return true;
        }
        else if (basementLeftDoor.transform.position == basementLeftDoorOpenPos.transform.position
        && basementRightDoor.transform.position == basementRightDoorOpenPos.transform.position)
        {
            return false;
        }
        return false;
    }
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
            case ElevatorDoor.Basement:
                {
                    Vector3 leftStartPos = basementLeftDoor.transform.position;
                    Vector3 rightStartPos = basementRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        basementLeftDoor.transform.position = Vector3.Slerp(leftStartPos, basementLeftDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        basementRightDoor.transform.position = Vector3.Slerp(rightStartPos, basementRightDoorOpenPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    basementLeftDoor.transform.position = basementLeftDoorOpenPos.transform.position;
                    basementRightDoor.transform.position = basementRightDoorOpenPos.transform.position;

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
                    Vector3 groundLeftStartPos = groundLeftDoor.transform.position;
                    Vector3 basementLeftStartPos = basementLeftDoor.transform.position;
                    Vector3 topRightStartPos = topRightDoor.transform.position;
                    Vector3 groundRightStartPos = groundRightDoor.transform.position;
                    Vector3 basementRightStartPos = basementRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        topLeftDoor.transform.position = Vector3.Slerp(topLeftStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        topRightDoor.transform.position = Vector3.Slerp(topRightStartPos, topDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        groundLeftDoor.transform.position = Vector3.Slerp(groundLeftStartPos, groundDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        groundRightDoor.transform.position = Vector3.Slerp(groundRightStartPos, groundDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        basementLeftDoor.transform.position = Vector3.Slerp(basementLeftStartPos, basementDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        basementRightDoor.transform.position = Vector3.Slerp(basementRightStartPos, basementDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    topLeftDoor.transform.position = topDoorClosedPos.transform.position;
                    topRightDoor.transform.position = topDoorClosedPos.transform.position;
                    groundLeftDoor.transform.position = groundDoorClosedPos.transform.position;
                    groundRightDoor.transform.position = groundDoorClosedPos.transform.position;
                    basementLeftDoor.transform.position = basementDoorClosedPos.transform.position;
                    basementRightDoor.transform.position = basementDoorClosedPos.transform.position;

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
            case ElevatorDoor.Basement:
                {
                    Vector3 leftStartPos = basementLeftDoor.transform.position;
                    Vector3 rightStartPos = basementRightDoor.transform.position;
                    float elapsedTime = 0f;

                    while (elapsedTime < doorMovementDuration)
                    {
                        basementLeftDoor.transform.position = Vector3.Slerp(leftStartPos, basementDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        basementRightDoor.transform.position = Vector3.Slerp(rightStartPos, basementDoorClosedPos.transform.position, elapsedTime / doorMovementDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    //Make sure the position is exact
                    basementLeftDoor.transform.position = basementDoorClosedPos.transform.position;
                    basementRightDoor.transform.position = basementDoorClosedPos.transform.position;

                    //Movement complete
                    runningCoroutine = null;
                }
                break;
            default:
                runningCoroutine = null;
                break;
        }
    }
    public override void CarriageButtonPress()
    {
        if (runningCoroutine != null || elevatorMoving) return;

        elevatorMoving = true;

        switch (carriagePos)
        {
            case CarriagePosition.Top:
                runningCoroutine = StartCoroutine(BringCarriageDown());
                ChangeLights(ElevatorLights.Up);
                break;
            case CarriagePosition.Ground:
                runningCoroutine = StartCoroutine(BringCarriageUp());
                ChangeLights(ElevatorLights.Down);
                break;
            case CarriagePosition.Basement:
                runningCoroutine = StartCoroutine(ReturnCarriageToGround());
                ChangeLights(ElevatorLights.Down);
                break;
            default:
                break;
        }
    }
    // public void BasementButtonPress()
    // {
    //     if (runningCoroutine != null) return;

    //     if (carriagePos == CarriagePosition.Basement && BasementDoorsClosed())
    //     {
    //         runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Basement));
    //     }
    //     else if (carriagePos == CarriagePosition.Basement && !BasementDoorsClosed())
    //     {
    //         runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.Basement));
    //     }
    //     ChangeLights(ElevatorLights.Down);
    // }
    IEnumerator CarriageToBasement()
    {
        if (!GroundDoorsClosed() || !TopDoorsClosed() || !BasementDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.All));
            yield return new WaitForSeconds(doorMovementDuration + 1f);
            EnableCarriageLights(false);
            Player.Instance.MovementEnabled = false;
            yield return new WaitForSeconds(2f);

        }
        runningCoroutine = StartCoroutine(MoveCarriageToBasement());

        yield return new WaitForSeconds(1f);
        runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Basement));
    }
    IEnumerator MoveCarriageToBasement()
    {
        //Make sure the position is exact
        carriage.transform.position = basementCarriagePos.transform.position;
        carriagePos = CarriagePosition.Basement;
        EnableCarriageLights(true);
        Player.Instance.MovementEnabled = true;

        //Movement complete
        runningCoroutine = null;
        elevatorMoving = false;
        yield return null;
    }
    IEnumerator ReturnCarriageToGround()
    {
        if (!GroundDoorsClosed() || !TopDoorsClosed() || !BasementDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.All));
            yield return new WaitForSeconds(doorMovementDuration + 1f);
            EnableCarriageLights(false);
            Player.Instance.MovementEnabled = false;
            yield return new WaitForSeconds(2f);
        }

        runningCoroutine = StartCoroutine(MoveCarriageToGround());

        yield return new WaitForSeconds(1f);
        runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Ground));
    }
    IEnumerator MoveCarriageToGround()
    {
        //Make sure the position is exact
        carriage.transform.position = groundCarriagePos.transform.position;
        carriagePos = CarriagePosition.Ground;
        EnableCarriageLights(true);
        Player.Instance.MovementEnabled = true;

        //Movement complete
        runningCoroutine = null;
        elevatorMoving = false;
        yield return null;
    }
    public void ToBasement()
    {
        if (runningCoroutine != null || elevatorMoving || carriagePos == CarriagePosition.Basement) return;
        elevatorMoving = true;
        runningCoroutine = StartCoroutine(CarriageToBasement());
        ChangeLights(ElevatorLights.Down);
    }
    private void EnableCarriageLights(bool enableLights)
    {
        if (carriageLights.Length == 0) return;

        foreach (var light in carriageLights)
        {
            light.enabled = enableLights;
        }
    }



}