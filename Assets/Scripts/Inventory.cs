using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> Items = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Items.Add(new Key() { Name = "PlayerRoomKey", KeyId = "1" });
        Items.Add(new Key() { Name = "StaffRoomKey", KeyId = "2" });
    }
    public bool InventoryContainsKey(string keyId)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] is Key)
            {
                var key = (Key)Items[i];
                if (key.KeyId == keyId) return true;
            }
        }

        return false;
    }

}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite Sprite;
}

[System.Serializable]
public class Key : Item
{
    public string KeyId;
}
