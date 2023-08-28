using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : NPC
{
    [SerializeField] private Door securityRoomDoor;
    [SerializeField] private Door elevatorEntranceDoor;
    [SerializeField] private Door kitchenDoor;
    [SerializeField] private Transform[] movementLocations;
    [SerializeField] private float moveSpeed;
    public void StartMovement()
    {
        StartCoroutine(MoveToKitchen());
    }
    private IEnumerator MoveToKitchen()
    {
        float duration = 0f;

        for (int i = 0; i < 5; i++)
        {
            duration = Vector3.Distance(movementLocations[i].position, movementLocations[i + 1].position) / moveSpeed;
            StartCoroutine(Move(movementLocations[i].position, movementLocations[i + 1].position, duration));
            StartCoroutine(Rotate(movementLocations[i + 1].position));
            yield return new WaitForSeconds(duration);
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
}
