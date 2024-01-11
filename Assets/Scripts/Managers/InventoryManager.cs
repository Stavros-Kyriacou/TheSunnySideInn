using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UI;
using UnityEngine.UI;


namespace Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        private static int maxStaffRoomEvidence = 4;
        public List<ItemData> Items = new List<ItemData>(12);
        [SerializeField] private List<InventorySlot> InventorySlots;
        [SerializeField] private RectTransform inventoryBackground;
        [SerializeField] private GridLayoutGroup inventoryLayoutGroup;

        [Header("Staff Room Evidence")]
        [SerializeField] private List<ItemData> staffRoomEvidenceList = new List<ItemData>(maxStaffRoomEvidence);
        private int staffRoomEvidenceIndex = 0;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            ResizeInventoryBackground();
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
        public void AddItem(ItemData itemData)
        {
            Items.Add(itemData);

            DrawInventory();
        }
        public void RemoveItem(Item item)
        {
            var itemToRemove = Items.SingleOrDefault(r => r.Name == item.itemData.Name);

            if (itemToRemove == null) return;

            Items.Remove(itemToRemove);
            DrawInventory();
        }
        public void AddStaffRoomEvidence()
        {
            AddItem(staffRoomEvidenceList[staffRoomEvidenceIndex]);

            if (staffRoomEvidenceIndex < 3)
            {
                staffRoomEvidenceIndex++;
            }
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
        private void ResizeInventoryBackground()
        {
            float topPadding = inventoryLayoutGroup.padding.top;
            float bottomPadding = inventoryLayoutGroup.padding.bottom;
            float leftPadding = inventoryLayoutGroup.padding.left;
            float rightPadding = inventoryLayoutGroup.padding.right;
            float xCellSize = inventoryLayoutGroup.cellSize.x;
            float yCellSize = inventoryLayoutGroup.cellSize.y;
            float width = (xCellSize * InventorySlots.Count()) + (inventoryLayoutGroup.spacing.x * (InventorySlots.Count() - 1)) + (leftPadding + rightPadding);
            float height = yCellSize + topPadding + bottomPadding;
            inventoryBackground.sizeDelta = new Vector2(width, height);
        }
    }
}