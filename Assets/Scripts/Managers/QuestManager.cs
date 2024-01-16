using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance;
        [SerializeField] private GameObject questPrefab;
        [SerializeField] private GameObject questPanel;
        [SerializeField] private List<QuestSlot> activeQuests;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        public void AddQuest(Quest quest)
        {
            //Create quest slot and set parent to quest panel
            GameObject newQuest = Instantiate(questPrefab);
            newQuest.transform.SetParent(questPanel.transform);

            //Add questslot to active quest list. setup quest slot
            QuestSlot newQuestSlot = newQuest.GetComponent<QuestSlot>();
            activeQuests.Add(newQuestSlot);
            newQuestSlot.SetupQuestSlot(quest);

            UIManager.Instance.DisplayNotifyText("Objectives updated");
        }

        public void CompleteQuest(Quest quest)
        {
            for (int i = 0; i < activeQuests.Count; i++)
            {
                if (activeQuests[i].quest.Description == quest.Description)
                {
                    Destroy(activeQuests[i].gameObject);
                    activeQuests.Remove(activeQuests[i]);
                }
            }
        }
    }
}
