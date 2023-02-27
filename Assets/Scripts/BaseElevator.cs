using System.Collections;
using UnityEngine;

public class BaseElevator : MonoBehaviour
{
    [Header("Carriage")]
    [SerializeField] protected Carriage carriage;
    [SerializeField] protected float carriageMovementDuration;

    [Header("Doors")]
    [SerializeField] protected Transform topLeftDoor;
    [SerializeField] protected Transform topRightDoor;
    [SerializeField] protected Transform groundLeftDoor;
    [SerializeField] protected Transform groundRightDoor;
    [SerializeField] protected float doorMovementDuration;

    [Header("Positions")]
    [SerializeField] protected Transform topCarriagePos;
    [SerializeField] protected Transform topDoorClosedPos;
    [SerializeField] protected Transform topLeftDoorOpenPos;
    [SerializeField] protected Transform topRightDoorOpenPos;
    [SerializeField] protected Transform groundCarriagePos;
    [SerializeField] protected Transform groundDoorClosedPos;
    [SerializeField] protected Transform groundLeftDoorOpenPos;
    [SerializeField] protected Transform groundRightDoorOpenPos;

    [Header("Up/Down Lights")]
    [SerializeField] protected Material lightOnMat;
    [SerializeField] protected Material lightOffMat;
    [SerializeField] protected MeshRenderer[] downArrows;
    [SerializeField] protected MeshRenderer[] upArrows;

    [SerializeField] protected Coroutine runningCoroutine;
    [SerializeField] protected bool elevatorMoving = false;
    [SerializeField] protected CarriagePosition carriagePos;
    protected virtual void Start()
    {
        carriagePos = CarriagePosition.Ground;
        ChangeLights(ElevatorLights.Down);
    }
    protected bool GroundDoorsClosed()
    {
        if (groundLeftDoor.transform.position == groundDoorClosedPos.transform.position
        && groundRightDoor.transform.position == groundDoorClosedPos.transform.position)
        {
            return true;
        }
        else if (groundLeftDoor.transform.position == groundLeftDoorOpenPos.transform.position
        && groundRightDoor.transform.position == groundRightDoorOpenPos.transform.position)
        {
            return false;
        }
        return false;
    }
    protected bool TopDoorsClosed()
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
    public virtual void GroundButtonPress()
    {
        if (runningCoroutine != null) return;

        if (carriagePos == CarriagePosition.Ground && GroundDoorsClosed())
        {
            runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Ground));
        }
        else if (carriagePos == CarriagePosition.Top && GroundDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageDown());
        }
        else if (carriagePos == CarriagePosition.Ground && !GroundDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.Ground));
        }
        else if (carriagePos == CarriagePosition.Top && !GroundDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageDown());
        }
        ChangeLights(ElevatorLights.Down);
    }
    public virtual void TopButtonPress()
    {
        if (runningCoroutine != null) return;

        if (carriagePos == CarriagePosition.Top && TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Top));
        }
        else if (carriagePos == CarriagePosition.Ground && TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageUp());
        }
        else if (carriagePos == CarriagePosition.Top && !TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.Top));
        }
        else if (carriagePos == CarriagePosition.Ground && !GroundDoorsClosed())
        {
            runningCoroutine = StartCoroutine(BringCarriageUp());
        }
        ChangeLights(ElevatorLights.Up);
    }
    public virtual void CarriageButtonPress()
    {
        if (runningCoroutine != null || elevatorMoving) return;

        elevatorMoving = true;

        if (carriagePos == CarriagePosition.Ground)
        {
            runningCoroutine = StartCoroutine(BringCarriageUp());
            ChangeLights(ElevatorLights.Up);
        }
        else if (carriagePos == CarriagePosition.Top)
        {
            runningCoroutine = StartCoroutine(BringCarriageDown());
            ChangeLights(ElevatorLights.Down);
        }
    }
    protected virtual IEnumerator OpenDoors(ElevatorDoor door)
    {
        return null;
    }
    protected virtual IEnumerator CloseDoors(ElevatorDoor door)
    {
        return null;
    }
    public IEnumerator BringCarriageUp()
    {
        if (!GroundDoorsClosed() || !TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.All));
            yield return new WaitForSeconds(doorMovementDuration + 1f);
        }
        runningCoroutine = StartCoroutine(CarriageUp());
        yield return new WaitForSeconds(carriageMovementDuration + 1f);
        runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Top));
    }
    public IEnumerator BringCarriageDown()
    {
        if (!GroundDoorsClosed() || !TopDoorsClosed())
        {
            runningCoroutine = StartCoroutine(CloseDoors(ElevatorDoor.All));
            yield return new WaitForSeconds(doorMovementDuration + 1f);
        }
        runningCoroutine = StartCoroutine(CarriageDown());
        yield return new WaitForSeconds(carriageMovementDuration + 1f);
        runningCoroutine = StartCoroutine(OpenDoors(ElevatorDoor.Ground));
    }
    IEnumerator CarriageUp()
    {
        Vector3 startPos = carriage.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < carriageMovementDuration)
        {
            carriage.transform.position = Vector3.Lerp(startPos, topCarriagePos.transform.position, elapsedTime / carriageMovementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Make sure the position is exact
        carriage.transform.position = topCarriagePos.transform.position;
        carriagePos = CarriagePosition.Top;

        //Movement complete
        runningCoroutine = null;
    }
    IEnumerator CarriageDown()
    {
        Vector3 startPos = carriage.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < carriageMovementDuration)
        {
            carriage.transform.position = Vector3.Lerp(startPos, groundCarriagePos.transform.position, elapsedTime / carriageMovementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Make sure the position is exact
        carriage.transform.position = groundCarriagePos.transform.position;
        carriagePos = CarriagePosition.Ground;

        //Movement complete
        runningCoroutine = null;
    }
    protected void ChangeLights(ElevatorLights direction)
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

public enum CarriagePosition
{
    Top,
    Ground,
    Basement
}
public enum ElevatorDoor
{
    Ground,
    Top,
    Basement,
    All
}
public enum ElevatorLights
{
    Up,
    Down
}