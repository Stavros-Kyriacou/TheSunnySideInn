using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Quest")]
    public class Quest : ScriptableObject
    {
        public int Id;
        public string Description;
    }
}