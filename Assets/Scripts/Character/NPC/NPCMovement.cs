using System.Collections;
using UnityEngine;
using Audio;
using Objects;
using Character.Components;
using Character.CameraControl;

namespace Character.NPC
{
    public class NPCMovement : MonoBehaviour
    {
        private Animator animator;
        private PlaySound playSound;
        [Header("NPC Type")]
        [SerializeField] private NPCType NPCType;

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

        [Header("Security Sequence")]
        [SerializeField] private Transform securityScareEndPosition;
        [SerializeField] private float securityScareMovementDuration;

        [Header("Dumpster Sequence")]
        [SerializeField] private float dumpsterScareDuration;

        [Header("Player Room Sequence")]
        [SerializeField] private float playerRoomMovementDurationUp;
        [SerializeField] private float playerRoomLingerDuration;
        [SerializeField] private float playerRoomMovementDurationDown;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playSound = GetComponent<PlaySound>();

            switch (NPCType)
            {
                case NPCType.SamathaBasement:
                    gameObject.SetActive(false);
                    break;
                case NPCType.SamanthaSecurity:
                    PlaySecurityScareSequence();
                    break;
                case NPCType.SamanthaDumpster:
                    PlayDumpterScareSequence();
                    break;
                case NPCType.SamanthaPlayerRoom:
                    PlayPlayerRoomScareSequence();
                    break;
                default:
                    Debug.LogError("NPC type not defined");
                    break;
            }
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

            //Calculate converted yrotation of player, in case they have spun around multiple times
            if (yRot <= 0)
            {
                convertedYRot = (yRot % 360) + 360;
            }
            else if (yRot >= 360)
            {
                convertedYRot = yRot % 360;
            }
            else
            {
                convertedYRot = yRot;
            }

            if ((convertedYRot > 115 || convertedYRot < 65) || (xRot < -40 || xRot > 50))
            {
                //Not looking forward, rotate speed slower
                rotatePlayerTime = 0.3f;
            }
            else
            {
                //Looking forward, rotate speed faster
                rotatePlayerTime = 0.1f;
            }

            //Change rotation to converted number so the player doesnt spin around multiple times
            Player.Instance.cameraController.RotateCamera(xRot, convertedYRot);
            yield return new WaitForEndOfFrame();

            //Rotate over time
            Player.Instance.cameraController.RotateCameraOverTime(0, 90, rotatePlayerTime, false);
            yield return new WaitForSeconds(rotatePlayerTime);

            //set end position of jump
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f, Player.Instance.transform.position.z);

            //fly towards player
            StartCoroutine(Move(startPosition, endPosition, basementMovementDuration));

            //Animation is triggered by overlap event trigger
            moveCamera.ToggleAnimationCamera(true);

            yield return new WaitForSeconds(basementMovementDuration);
        }
        public void PlaySecurityScareSequence()
        {
            gameObject.SetActive(true);
            animator.Play("Crawl_Backwards");
            StartCoroutine(SecurityScareRoutine());
        }
        private IEnumerator SecurityScareRoutine()
        {
            StartCoroutine(Move(transform.position, securityScareEndPosition.position, securityScareMovementDuration));
            yield return new WaitForSeconds(securityScareMovementDuration);
            gameObject.SetActive(false);

            yield return null;
        }
        public void PlayDumpterScareSequence()
        {
            StartCoroutine(DumpsterScareRoutine());
        }
        private IEnumerator DumpsterScareRoutine()
        {
            animator.Play("Hallway_Scare");
            playSound.Play();
            yield return new WaitForSeconds(dumpsterScareDuration);
            gameObject.SetActive(false);
            Player.Instance.EnableMovement(true);
            yield return null;
        }
        public void PlayPlayerRoomScareSequence()
        {
            StartCoroutine(PlayerRoomScareRoutine());
        }
        private IEnumerator PlayerRoomScareRoutine()
        {
            Vector3 starPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);

            animator.Play("Hallway_Scare");

            //Move up
            StartCoroutine(Move(starPos, endPos, playerRoomMovementDurationUp));
            yield return new WaitForSeconds(playerRoomMovementDurationUp);
            playSound.Play();
            yield return new WaitForSeconds(playerRoomLingerDuration);

            //Move down
            StartCoroutine(Move(endPos, starPos, playerRoomMovementDurationDown));
            yield return new WaitForSeconds(playerRoomMovementDurationDown);
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
}
public enum NPCType
{
    SamathaBasement,
    SamanthaSecurity,
    SamanthaDumpster,
    SamanthaPlayerRoom
}