using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : NPC
{
    [SerializeField] private Door[] doors;
    [SerializeField] private Transform[] movementLocations;
    [SerializeField] private Transform kitchenDoorCollider;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Door[] lockers;
    public void StartMovement()
    {
        StartCoroutine(MoveToKitchen());
    }
    private IEnumerator MoveToKitchen()
    {
        float duration = 0f;

        for (int i = 0; i < movementLocations.Length - 1; i++)
        {
            duration = Vector3.Distance(movementLocations[i].position, movementLocations[i + 1].position) / moveSpeed;
            StartCoroutine(Move(movementLocations[i].position, movementLocations[i + 1].position, duration));
            StartCoroutine(Rotate(movementLocations[i + 1].position));

            //close the kitchen doors after entering
            if (i == 5)
            {
                if (doors[2].IsOpen)
                {
                    doors[2].CloseDoor();
                }
            }

            yield return new WaitForSeconds(duration);
        }

        kitchenDoorCollider.gameObject.SetActive(false);
        gameObject.SetActive(false);
        GameManager.Instance.Security_Guard_Gone = true;

        for (int i = 0; i < lockers.Length; i++)
        {
            lockers[i].UnlockDoor();
        }

        yield return null;
    }
    private IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float movementDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / movementDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endPosition;

        foreach (var door in doors)
        {
            if (Vector3.Distance(door.transform.position, transform.position) < 2f && !door.IsOpen)
            {
                door.OpenDoor(0);
            }
        }

        yield return null;
    }
    private IEnumerator Rotate(Vector3 target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target - transform.position);

        float elapsedTime = 0f;

        while (elapsedTime < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    public void Rotate(Transform target)
    {
        StartCoroutine(Rotate(target.position));
    }
}
