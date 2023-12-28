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
    [SerializeField] private MoveCamera moveCamera;
    [SerializeField] private Animator cameraAnimator;

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
        

        float yRot = Player.Instance.cameraController.Y_Rotation;
        float xRot = Player.Instance.cameraController.X_Rotation;
        float convertedYRot = 0;
        float rotatePlayerTime = 0f;

        if (yRot < 0)
        {
            convertedYRot = (yRot % 360) + 360;
        }
        else if (yRot > 360)
        {
            convertedYRot = yRot % 360;
        }

        if ((convertedYRot > 205 || convertedYRot < 155) || (xRot < -40 || xRot > 50))
        {
            //Not looking forward
            Debug.Log("not looking forward");
            rotatePlayerTime = 0.3f;
        }
        else
        {
            //Looking forward
            Debug.Log("looking forward");
            rotatePlayerTime = 0.1f;
        }

        Player.Instance.cameraController.RotateCameraOverTime(0, 180, rotatePlayerTime, false);
        yield return new WaitForSeconds(rotatePlayerTime);

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f, Player.Instance.transform.position.z);

        //fly towards player
        StartCoroutine(Move(startPosition, endPosition, basementMovementDuration));

        //Animation is triggered by overlap event trigger
        moveCamera.ToggleAnimationCamera(true);

        yield return new WaitForSeconds(basementMovementDuration);
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
