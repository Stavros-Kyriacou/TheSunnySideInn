using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomTrigger : EventTrigger
{
    [SerializeField] NPCMovement samanthaMovementController;
    [SerializeField] private Transform destination;
    [SerializeField] private Transform wakeUpDestination;
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

    private IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(4.5f);

        //Disable player movement, rotate player, teleport player
        Player.Instance.EnableMovement(false);
        Player.Instance.InteractionEnabled = false;
        Player.Instance.CameraEnabled = false;
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.isKinematic = true;
        Player.Instance.cameraController.RotateCamera(0, 90);
        playerRigidBody.transform.position = destination.position;

        UIManager.Instance.FadeToBlack(false);

        Player.Instance.CameraEnabled = true;
        Player.Instance.SlowMovement(4f);

        yield return new WaitForSeconds(4f);

        playerRigidBody.isKinematic = false;
        Player.Instance.InteractionEnabled = true;

        yield return null;
    }
}
