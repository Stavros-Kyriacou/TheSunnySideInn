using System.Collections;
using UnityEngine;
using Character.NPC;
using Character.CameraControl;
using Character.Components;
using Objects;
using Managers;

namespace Triggers
{
    public class PlayerRoomTrigger : EventTrigger
    {
        [SerializeField] NPCMovement samanthaMovementController;
        [SerializeField] private Transform destination;
        [SerializeField] private Transform wakeUpDestination;
        [SerializeField] private MoveCamera moveCamera;
        [SerializeField] private Ladder ladder;
        [SerializeField] private Ladder basementLadder;
        private Rigidbody playerRigidBody;
        private void Start()
        {
            playerRigidBody = Player.Instance.GetComponent<Rigidbody>();
        }
        public void PlayerAwakeSequence()
        {
            //Disable event trigger
            this.EnterTriggerActive = false;

            //Disable player controls
            Player.Instance.EnableMovement(false);
            Player.Instance.CameraEnabled = false;

            //Play samantha jump to player animation
            samanthaMovementController.PlayBasementMovementSequence();

            //Fade camera to black
            UIManager.Instance.FadeToBlack(true);

            StartCoroutine(MovePlayer());
        }

        private IEnumerator MovePlayer()
        {
            yield return new WaitForSeconds(4.5f);

            //Disable player movement, rotate player, teleport player
            moveCamera.ToggleAnimationCamera(false);
            Player.Instance.EnableMovement(false);
            Player.Instance.InteractionEnabled = false;
            Player.Instance.CameraEnabled = false;
            playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.isKinematic = true;
            Player.Instance.cameraController.RotateCamera(0, 270);
            playerRigidBody.transform.position = destination.position;

            UIManager.Instance.FadeToBlack(false);

            Player.Instance.CameraEnabled = true;
            Player.Instance.SlowMovement(4f);

            yield return new WaitForSeconds(4f);

            playerRigidBody.isKinematic = false;
            Player.Instance.InteractionEnabled = true;

            //Reset elevator
            ladder.IsInteractable = false;
            basementLadder.IsInteractable = false;

            yield return null;
        }
    }
}