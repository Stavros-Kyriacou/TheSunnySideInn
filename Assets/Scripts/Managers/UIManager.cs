using System.Collections;
using UnityEngine;
using TMPro;
using Character.Components;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public UI_Controller UI_Controller;
        public Canvas canvas;
        public Animator canvasAnimator;
        [SerializeField] private RectTransform fadeToBlack;

        [Header("Inventory")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject itemViewPanel;
        [SerializeField] private GameObject scrollButtons;

        [Header("Notify Text")]
        [SerializeField] private TextMeshProUGUI notifyText;
        [SerializeField] private Color32 startColour = new Color32(255, 255, 255, 255);
        [SerializeField] private Color32 endColour = new Color32(255, 255, 255, 0);

        private float elapsedTime = 0f;
        private Coroutine fadingText = null;

        private void Awake()
        {
            Instance = this;
            canvasAnimator = canvas.GetComponent<Animator>();
            inventoryPanel.SetActive(false);
            itemViewPanel.SetActive(false);
            scrollButtons.SetActive(false);

            UI_Controller = new UI_Controller();
            UI_Controller.Map.Inventory.performed += x => ToggleInventory();

            UI_Controller.Enable();
        }
        public void DisplayNotifyText(string message)
        {
            if (fadingText != null)
            {
                StopAllCoroutines();
                notifyText.color = startColour;
                notifyText.enabled = false;
                fadingText = StartCoroutine(FadeText(message));
            }

            fadingText = StartCoroutine(FadeText(message));
        }
        private IEnumerator FadeText(string message)
        {
            elapsedTime = 0f;

            notifyText.enabled = true;
            notifyText.text = message;

            while (elapsedTime < 1)
            {
                notifyText.color = Color32.Lerp(startColour, endColour, elapsedTime / 1);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            notifyText.color = endColour;
            notifyText.text = "";

            fadingText = null;
        }
        public void FadeToBlack(bool fadeToBlack)
        {
            this.fadeToBlack.gameObject.SetActive(true);

            if (fadeToBlack)
            {
                canvasAnimator.Play("Fade_In_Black");
            }
            else
            {
                canvasAnimator.Play("Fade_Out_Black");
            }
        }

        private void ToggleInventory()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            Cursor.visible = !Cursor.visible;
            Player.Instance.CameraEnabled = !Player.Instance.CameraEnabled;
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            itemViewPanel.SetActive(false);
            scrollButtons.SetActive(false);
        }
        public void PlaySleepAnimation() 
        {
            canvasAnimator.Play("Sleep");
        }
    }
}