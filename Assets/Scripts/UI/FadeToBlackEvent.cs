using UnityEngine;

namespace UI
{
    public class FadeToBlackEvent : MonoBehaviour
    {
        [SerializeField] private RectTransform fadeToBlack;

        public void DisableFadeToBlack()
        {
            this.fadeToBlack.gameObject.SetActive(false);
        }
    }
}