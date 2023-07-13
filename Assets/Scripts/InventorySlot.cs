using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI labelText;
    [HideInInspector] public ItemData ItemData;

    public void ClearSlot()
    {
        EnableSlot(false);
        this.ItemData = null;
    }

    public void DrawSlot(ItemData itemData)
    {
        if (itemData == null)
        {
            ClearSlot();
            return;
        }

        EnableSlot(true);

        this.ItemData = itemData;
        icon.sprite = itemData.Icons[0];
        labelText.text = itemData.Name;
    }

    private void EnableSlot(bool enabled)
    {
        icon.enabled = enabled;
        labelText.enabled = enabled;
    }
}
