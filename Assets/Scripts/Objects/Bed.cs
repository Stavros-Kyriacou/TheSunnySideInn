using UnityEngine;
using Character.Components;

namespace Objects
{
    public class Bed : MonoBehaviour
    {
        [SerializeField] private Transform wakeUpDestination;
        [SerializeField] private float wakeUpMovementDuration;
        [SerializeField] private Collider bedCollider;
        public void WakeUp()
        {
            Player.Instance.MovePlayer(Player.Instance.transform.position, wakeUpDestination.position, wakeUpMovementDuration, true);
            Invoke("EnableBedCollider", wakeUpMovementDuration);
        }
        public void EnableBedCollider()
        {
            bedCollider.enabled = true;
        }


    }
}