using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Data;
using Common.Scripts.UI.Components;
using Kuhpik;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.UI
{
    public class DeliveryQuestsScreen : UIScreen
    {
        [SerializeField] private Transform questsParent;
        [SerializeField] private Transform arrowsParent;
        [SerializeField] private QuestStatusPanel questStatusPanelPrefab;

        [SerializeField] private Color[] questColors;
            
        private List<Color> availableColors = new List<Color>();
        private Dictionary<ActiveQuest, QuestStatusPanel> questStatusPanels = new Dictionary<ActiveQuest, QuestStatusPanel>();

        public override void Open()
        {
            base.Open();
            availableColors = questColors.ToList();
        }

        public void AddNewQuest(ActiveQuest activeQuest)
        {
            var questColor = availableColors[Random.Range(0, availableColors.Count)];
            var newStatusPanel = Instantiate(questStatusPanelPrefab, questsParent);
            newStatusPanel.Init(activeQuest.Requirement.ItemConfig.ItemIcon, activeQuest.Requirement.Amount,questColor);
            
            questStatusPanels.Add(activeQuest,newStatusPanel);
            
            availableColors.Remove(questColor);
        }

        public void RemoveQuest(ActiveQuest activeQuest)
        {
            var questPanel = questStatusPanels[activeQuest];
            questStatusPanels.Remove(activeQuest);
            availableColors.Add(questPanel.Color);
            
            Destroy(questPanel.gameObject);
        }

        private void LateUpdate()
        {
            foreach (var questStatusPanel in questStatusPanels)
            {
                var activeQuest = questStatusPanel.Key;
                questStatusPanel.Value.UpdateTimer(activeQuest.RemainingTime/activeQuest.GivenTime);
            }
        }

    }
}