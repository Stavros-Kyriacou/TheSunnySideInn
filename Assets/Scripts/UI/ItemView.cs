using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemView : MonoBehaviour
    {
        private Image viewImage;
        private InventorySlot inventorySlot;
        [SerializeField] private GameObject itemViewPanel;
        [SerializeField] private GameObject scrollButtons;
        private int currentIconIndex = 0;

        public void DisplayItem(InventorySlot inventorySlot)
        {
            if (inventorySlot.ItemData == null) return;

            if (viewImage == null)
            {
                this.viewImage = GetComponent<Image>();
            }

            if (viewImage == null) return;

            if (inventorySlot != null)
            {
                this.inventorySlot = inventorySlot;
            }

            if (this.inventorySlot.ItemData.Icons.Length > 1)
            {
                scrollButtons.SetActive(true);
            }
            else
            {
                scrollButtons.SetActive(false);
            }

            currentIconIndex = 0;
            itemViewPanel.SetActive(true);
            viewImage.sprite = inventorySlot.ItemData.Icons[currentIconIndex];
        }
        public void NextImage()
        {
            //inventory slot must exist
            if (this.inventorySlot == null) return;

            //must have mroe than 1 icon
            if (this.inventorySlot.ItemData.Icons.Length <= 1) return;

            if (currentIconIndex + 1 < this.inventorySlot.ItemData.Icons.Length)
            {
                currentIconIndex++;
                viewImage.sprite = inventorySlot.ItemData.Icons[currentIconIndex];
            }
        }

        public void PreviousImage()
        {
            //inventory slot must exist
            if (this.inventorySlot == null) return;

            //must have mroe than 1 icon
            if (this.inventorySlot.ItemData.Icons.Length <= 1) return;

            if (currentIconIndex > 0)
            {
                currentIconIndex--;
                viewImage.sprite = inventorySlot.ItemData.Icons[currentIconIndex];
            }
        }
    }
}