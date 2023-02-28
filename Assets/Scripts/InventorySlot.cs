using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI labelText;

    public void ClearSlot()
    {
        EnableSlot(false);
    }

    public void DrawSlot(ItemData itemData)
    {
        if (itemData == null)
        {
            ClearSlot();
            return;
        }

        EnableSlot(true);

        icon.sprite = itemData.Icon;
        labelText.text = itemData.Name;

    }

    private void EnableSlot(bool enabled)
    {
        icon.enabled = enabled;
        labelText.enabled = enabled;
    }
}
