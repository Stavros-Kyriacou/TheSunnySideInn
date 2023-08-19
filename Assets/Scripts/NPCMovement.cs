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

    [Header("Basement Sequence")]
    [SerializeField] private Transform basementPosition1;
    [SerializeField] private Transform basementPosition2;
    [SerializeField] private float basementMovementDuration;

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
        transform.position = securityStartPos.position;
        yield return new WaitForSeconds(startDelay);

        animator.SetTrigger("OnCrawl");
        StartCoroutine(Move(securityStartPos.position, securityRoomPos.position, securityMovementDuration));
        yield return new WaitForSeconds(securityMovementDuration + 2f);
        animator.SetTrigger("OnStop");

        securityDoor.PlayHandleAnimation();

        yield return new WaitForSeconds(2f);

        animator.SetTrigger("OnCrawl");
        StartCoroutine(Move(securityRoomPos.position, securityEndPos.position, securityMovementDuration));
        yield return new WaitForSeconds(securityMovementDuration);
        animator.SetTrigger("OnStop");

        securityDoor.UnlockDoor();
        gameObject.SetActive(false);

    }
    public void PlayBasementStartAnimation()
    {
        gameObject.SetActive(true);
        animator.Play("Hallway_Scare");
    }
    public void PlayBasementMovementSequence()
    {
        animator.Play("Crawl_Backwards");
        StartCoroutine(BasementMovement());
    }
    private IEnumerator BasementMovement()
    {
        Vector3 startPosition = transform.position;

        //Move to first position
        StartCoroutine(Move(startPosition, basementPosition1.position, basementMovementDuration / 2));
        yield return new WaitForSeconds(basementMovementDuration / 2);

        //Move to second position
        StartCoroutine(Move(transform.position, basementPosition2.position, basementMovementDuration / 2));
        yield return new WaitForSeconds(basementMovementDuration / 2);

        gameObject.SetActive(false);
    }
    private IEnumerator Move(Vector3 startPos, Vector3 endPos, float movementDuration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }


}
