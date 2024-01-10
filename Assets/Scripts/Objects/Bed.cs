using UnityEngine;
using Character.Components;
using Character.CameraControl;
using Managers;
using System.Collections;
using Character.NPC;
using Interfaces;
using TMPro;

namespace Objects
{
    public class Bed : MonoBehaviour
    {
        [SerializeField] private Transform wakeUpLocation;
        [SerializeField] private float wakeUpMovementDuration;
        [SerializeField] private Interactable wakeUpTrigger;
        [SerializeField] private Collider bedCollider;
        [SerializeField] private MoveCamera moveCamera;
        [SerializeField] private float windowTappingDelay;
        [SerializeField] private NPCMovement samanthaPlayerRoom;
        [SerializeField] private float sleepAnimationDuration;
        [SerializeField] private TextMeshPro alarmText1;
        [SerializeField] private TextMeshPro alarmText2;
        [SerializeField] private AnimationClip sleepClip;
        [SerializeField] private AnimationClip fadeOutBlackClip;

        void Awake()
        {
            alarmText1.gameObject.SetActive(true);
            alarmText2.gameObject.SetActive(false);
        }
        public void WakeUp()
        {
            //Move player to wake up destination
            //Set camera state to player
            //Rotate camera
            Player.Instance.RigidBody.MovePosition(new Vector3(wakeUpLocation.position.x, Player.Instance.transform.position.y, wakeUpLocation.position.z));
            moveCamera.CameraState = CameraState.PLAYER;
            Player.Instance.cameraController.RotateCamera(0, 270);
            Player.Instance.EnableMovement(true);
            Player.Instance.CameraEnabled = true;
            Player.Instance.InteractionEnabled = true;
        }
        public void EnableWakeUpTrigger()
        {
            wakeUpTrigger.gameObject.SetActive(true);
            wakeUpTrigger.IsInteractable = true;
        }
        public void SetBedCameraPosition()
        {
            Player.Instance.cameraController.RotateCamera(0, 270);
            moveCamera.CameraState = CameraState.BED;
        }
        public void Sleep()
        {
            Player.Instance.EnableMovement(false);
            Invoke("EnableWakeUpTrigger", sleepClip.length + fadeOutBlackClip.length);
            Invoke("ChangeAlarmTime", sleepClip.length);
            SetBedCameraPosition();
            UIManager.Instance.PlaySleepAnimation();
            StartCoroutine(PlayWindowTappingSoundRoutine());
        }
        private IEnumerator PlayWindowTappingSoundRoutine()
        {
            yield return new WaitForSeconds(windowTappingDelay);
            samanthaPlayerRoom.gameObject.SetActive(true);
        }
        private void ChangeAlarmTime()
        {
            alarmText1.gameObject.SetActive(false);
            alarmText2.gameObject.SetActive(true);
        }
    }
}