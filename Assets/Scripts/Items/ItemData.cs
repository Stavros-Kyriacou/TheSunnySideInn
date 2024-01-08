using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Item")]
    public class ItemData : ScriptableObject
    {
        public string Name;
        public Sprite[] Icons;
    }
}