using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> Items = new List<ItemData>(9);
    [SerializeField] private List<InventorySlot> InventorySlots;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DrawInventory();
    }
    public bool InventoryContainsKey(string keyId)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] is KeyData)
            {
                var key = (KeyData)Items[i];
                if (key.KeyId == keyId) return true;
            }
        }

        return false;
    }
    public void AddItem(Item item)
    {
        Items.Add(item.itemData);

        DrawInventory();
    }
    public void RemoveItem(Item item)
    {
        var itemToRemove = Items.SingleOrDefault(r => r.Name == item.itemData.Name);

        if (itemToRemove == null) return;

        Items.Remove(itemToRemove);
        DrawInventory();
    }
    public void DrawInventory()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlots[i].ClearSlot();
        }

        for (int i = 0; i < Items.Count; i++)
        {
            InventorySlots[i].DrawSlot(Items[i]);
        }
    }
}
