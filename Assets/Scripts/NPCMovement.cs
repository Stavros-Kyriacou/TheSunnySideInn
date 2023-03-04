using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private Animator animator;
    [Header("Security Room Scene")]
    [SerializeField] private Door securityDoor;
    [SerializeField] private Transform securityStartPos;
    [SerializeField] private Transform securityRoomPos;
    [SerializeField] private Transform securityEndPos;
    [SerializeField] private float startDelay;
    [SerializeField] private float securityMovementDuration;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void PlaySecurityRoomMovement()
    {
        gameObject.SetActive(true);

        StartCoroutine(SecurityRoomSequence());
    }

    private IEnumerator SecurityRoomSequence()
    {
        //move to security location
        //rotate towards door
        //trigger animation
        //rotate back
        //move to end pos

        transform.position = securityStartPos.position;
        yield return new WaitForSeconds(startDelay);


        StartCoroutine(Move(securityStartPos.position, securityRoomPos.position, securityMovementDuration));

        yield return new WaitForSeconds(securityMovementDuration + 2f);

        securityDoor.PlayHandleAnimation();

        yield return new WaitForSeconds(2f);

        StartCoroutine(Move(securityRoomPos.position, securityEndPos.position, securityMovementDuration));

        yield return new WaitForSeconds(securityMovementDuration);
        gameObject.SetActive(false);

    }
    private IEnumerator Move(Vector3 startPos, Vector3 endPos, float movementDuration)
    {
        animator.SetTrigger("OnCrawl");
        float elapsedTime = 0f;
        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        animator.SetTrigger("OnStop");
    }


}
