using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<IGameEventListener> eventListeners = new List<IGameEventListener>();

        public void Raise()
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                eventListeners[i].OnEventRaised();
            }
        }
        public void RegisterListener(IGameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }
        public void UnregisterListener(IGameEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }

    }
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (GameEvent)target;

            if (GUILayout.Button("Raise Event", GUILayout.Height(40)))
            {
                script.Raise();
            }
        }
    }

}
