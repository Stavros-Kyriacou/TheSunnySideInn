using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class QuestSlot : MonoBehaviour
    {
        public Quest quest;

        public void SetupQuestSlot(Quest quest)
        {
            this.quest = quest;
            TextMeshProUGUI questText = GetComponent<TextMeshProUGUI>();
            questText.text = "- " + quest.Description;
        }

    }
}
