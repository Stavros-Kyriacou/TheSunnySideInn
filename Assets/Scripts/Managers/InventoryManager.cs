using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UI;
using UnityEngine.UI;
using UnityEngine.Events;
using Events;


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
        [SerializeField] private ItemData missingPersonsReport;

        [Header("Staff Room Evidence")]
        [SerializeField] private List<ItemData> staffRoomEvidenceList = new List<ItemData>(maxStaffRoomEvidence);
        public UnityEvent OnAllEvidenceFound;
        private int staffRoomEvidenceIndex = 0;
        [Header("Staff Room Quests and Events")]
        [SerializeField] private List<Quest> staffRoomQuests = new List<Quest>();
        [SerializeField] private List<GameEvent> staffRoomQuestEvents = new List<GameEvent>();
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            AddItem(missingPersonsReport);
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

            //Complete quest if all evidence has been found
            if (staffRoomEvidenceIndex == 3)
            {
                OnAllEvidenceFound.Invoke();
            }

            //Add individual quests for each note and raise events to enable/disable gameobjects
            if (staffRoomEvidenceIndex > 0)
            {
                QuestManager.Instance.AddQuest(staffRoomQuests[staffRoomEvidenceIndex - 1]);
                staffRoomQuestEvents[staffRoomEvidenceIndex - 1].Raise();
            }

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