using UnityEngine;
using Character.Components;

namespace Objects
{
    public class Carriage : MonoBehaviour
    {
        public Player player;
        [SerializeField] private Ladder ladder;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.transform.parent.parent = this.transform;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                other.transform.parent.parent = null;
            }
        }
        public void HatchOpenAnimComplete()
        {
            ladder.IsInteractable = true;
        }
    }
}