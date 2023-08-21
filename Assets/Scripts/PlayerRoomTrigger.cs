using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomTrigger : EventTrigger
{
    [SerializeField] NPCMovement samanthaMovementController;
    [SerializeField] private Transform destination;
    private Rigidbody playerRigidBody;
    private void Start()
    {
        playerRigidBody = Player.Instance.GetComponent<Rigidbody>();
    }
    public void PlayerAwakeSequence()
    {
        //Disable event trigger
        this.EnterTriggerActive = false;

        //Play samantha crawl away animation
        samanthaMovementController.PlayBasementMovementSequence();

        //Slow player movement
        Player.Instance.SlowMovement(4f);

        //Fade camera to black
        UIManager.Instance.FadeToBlack(true);

        StartCoroutine(MovePlayer());
    }
    private void TeleportPlayerToRoom()
    {
        // Player.Instance.ColliderEnabled = false;
        playerRigidBody.isKinematic = true;
        Player.Instance.MovementEnabled = false;
        Player.Instance.InteractionEnabled = false;
        Player.Instance.CameraEnabled = false;
        // Rigidbody playerRigidBody = Player.Instance.GetComponent<Rigidbody>();
        playerRigidBody.transform.position = destination.position;
        // playerRigidBody.isKinematic = false;
        Player.Instance.CameraEnabled = true;
        // UIManager.Instance.FadeToBlack(false);
        // StartCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(4.5f);

        //Disable player movement, rotate player, teleport player
        Player.Instance.InteractionEnabled = false;
        Player.Instance.CameraEnabled = false;
        Player.Instance.EnableMovement(false);
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.isKinematic = true;
        Player.Instance.cameraController.RotateCamera(0, 90);
        playerRigidBody.transform.position = destination.position;


        UIManager.Instance.FadeToBlack(false);

        Player.Instance.SlowMovement(4f);

        yield return new WaitForSeconds(4f);

        Player.Instance.CameraEnabled = true;
        playerRigidBody.isKinematic = false;

        yield return null;
    }


}
