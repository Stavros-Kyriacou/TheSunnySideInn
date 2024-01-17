using UnityEngine;
using Managers;
using Character.Components;
using Character.CameraControl;
using Interfaces;
using Items;
using Audio;
using JetBrains.Annotations;

namespace Objects
{
    public class Taxi : MonoBehaviour
    {
        [SerializeField] private Transform playerSitPosition;
        [SerializeField] private MoveCamera moveCamera;
        [SerializeField] private PlaySound phone;
        [SerializeField] private Interactable leaveTaxiTrigger;
        [SerializeField] private Item luggage;
        [SerializeField] private float cameraRotateTime;
        private Animator animator;
        //TODO - add sound effect of taxi driving off
        void Awake()
        {
            animator = GetComponent<Animator>();
            leaveTaxiTrigger.IsInteractable = false;
        }
        public void StartDriving()
        {
            moveCamera.CameraState = CameraState.TAXI;
            moveCamera.gameObject.transform.SetParent(gameObject.transform);
            Player.Instance.cameraController.RotateCamera(0, 180);
            Player.Instance.MovementEnabled = false;
            animator.SetTrigger("OnDrive");
        }
        public void ReceivePhoneCall()
        {
            phone.Play();
        }
        public void LeaveTaxi()
        {
            //Move player to line up with camera position, make camera follow player, make camera face building, add luggage to inventory
            moveCamera.CameraState = CameraState.PLAYER;
            moveCamera.transform.SetParent(null);
            Player.Instance.cameraController.RotateCamera(0, 0);
            Player.Instance.MovementEnabled = true;
            luggage.Interact();
            phone.gameObject.SetActive(false);
            animator.SetTrigger("OnDriveAway");
        }
        public void HideTaxi()
        {
            gameObject.SetActive(false);
        }
        public void RotateCameraWhileDriving()
        {
            Player.Instance.cameraController.RotateCameraOverTime(Player.Instance.cameraController.X_Rotation, Player.Instance.cameraController.Y_Rotation - 90, cameraRotateTime, true);
        }
    }
}