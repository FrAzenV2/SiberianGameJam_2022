using System;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Components;
using Common.Scripts.Data;
using Common.Scripts.ScriptableObjects;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Common.Scripts.Systems
{
    public class DeliveryQuestSystem : GameSystem
    {
        private DeliverySystemConfig deliverySystemConfig;
     
        private bool isPlayerInZone;
        
        private List<DeliveryPointComponent> deliveryPoints;
        private List<ActiveQuest> activeQuests = new List<ActiveQuest>();
        private List<ActiveQuest> failedQuests = new List<ActiveQuest>();
        public override void OnInit()
        {
            deliveryPoints = FindObjectOfType<DeliveryPointsContainer>().DeliveryPoints;
        }

        public override void OnStateEnter()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered += TryFinishQuest;
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered += PlayerEnteredQuestZone;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited += PlayerExitedQuestZone;
        }
        public override void OnStateExit()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered -= TryFinishQuest;
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered -= PlayerEnteredQuestZone;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited -= PlayerExitedQuestZone;
        }
        
        public override void OnUpdate()
        {
            CheckIfPlayerWantAcceptQuest();
            UpdateActiveQuestsTimers();
            FindFailedQuests();
            ClearFailedQuests();
        }
        
        #region QuestUpdating

        private void ClearFailedQuests()
        {
            foreach (var quest in failedQuests)
            {
                FailQuest(quest);
            }

            activeQuests.RemoveAll(quest => failedQuests.Contains(quest));
            failedQuests.Clear();
        }
        private void FindFailedQuests()
        {
            foreach (var quest in activeQuests.Where(quest => quest.RemainingTime <= 0))
            {
                failedQuests.Add(quest);
            }
        }
        private void UpdateActiveQuestsTimers()
        {
            foreach (var quest in activeQuests)
            {
                quest.RemainingTime -= Time.deltaTime;
            }
        }
        private void FailQuest(ActiveQuest quest)
        {
            //TODO erase pointers for quests
            quest.Target.HighlightObject.SetActive(false);
            quest.Target.IsBusy = false;
            Bootstrap.GetSystem<StackSystem>().RemoveItem(quest.Requirement.ItemType, quest.Requirement.Amount);
        }

        #endregion
        
        #region QuestGiving
        
        private void PlayerEnteredQuestZone(GameObject obj)
        {
            if(!obj.TryGetComponent(out QuestGiverPointComponent questGiver)) return;

            //TODO add UI for quest picking
            isPlayerInZone = true;
        }

        private void PlayerExitedQuestZone(GameObject obj)
        {
            if(!obj.TryGetComponent(out QuestGiverPointComponent questGiver)) return;

            //TODO add UI for quest picking
            isPlayerInZone = false;
        }

        private void CheckIfPlayerWantAcceptQuest()
        {
            if (!(isPlayerInZone && game.InteractNextFrame)) return;
            if (activeQuests.Count >= deliverySystemConfig.MaxQuests) return;

            game.InteractNextFrame = false;
            TryStartNewDelivery();
        }

        private void TryStartNewDelivery()
        {
            var newTarget = GetFreeDeliveryPoint();
            if(!newTarget) return;

            StartNewDelivery(newTarget);
        }
        
        private void StartNewDelivery(DeliveryPointComponent newTarget)
        {
            var path = new NavMeshPath();
            
            if (!NavMesh.CalculatePath(game.PlayerEntity.transform.position, newTarget.TargetPosition.position, NavMesh.AllAreas, path))
            {
                Debug.Log($"NO AVAILABLE PATH TO TARGET{newTarget}");
                return;
            }

            var distance = CalculatePathDistance(path);
            var givenTime = distance * deliverySystemConfig.SecondsPerMeter;
            var reward = distance * deliverySystemConfig.RewardPerMeter;
            var requirement = GetDeliveryRequirement();

            newTarget.IsBusy = true;
            newTarget.HighlightObject.SetActive(true);
            //TODO add UI arrow to each quest
            activeQuests.Add(new ActiveQuest(givenTime, givenTime, newTarget, requirement, reward));
        }

        private DeliveryRequirement GetDeliveryRequirement()
        {
            //TODO maybe add some logic for more favourite quests;
            return deliverySystemConfig.DeliveryRequirements[Random.Range(0, deliverySystemConfig.DeliveryRequirements.Length)];
        }
        private DeliveryPointComponent GetFreeDeliveryPoint()
        {
            var availablePoints = deliveryPoints.Where(point => !point.IsBusy).ToList();

            return availablePoints.Count == 0 ? null : availablePoints[Random.Range(0, availablePoints.Count)];
        }

        private float CalculatePathDistance(NavMeshPath path)
        {
            float pathDistance = 0;
            for (var index = 0; index < path.corners.Length-1; index++)
            {
                var corner = path.corners[index];
                var nextCorner = path.corners[index + 1];
                pathDistance += Vector3.Distance(corner, nextCorner);
            }
            return pathDistance;
        }

        #endregion
        
        #region QuestFinishing

        private void TryFinishQuest(GameObject obj)
        {
            if(!obj.TryGetComponent(out DeliveryPointComponent deliveryPointComponent)) return;

            foreach (var quest in activeQuests.Where(quest => quest.Target == deliveryPointComponent))
            {
                FinishQuest(quest);
                return;
            }
        }

        private void FinishQuest(ActiveQuest quest)
        {
            if(!Bootstrap.GetSystem<StackSystem>().RemoveItem(quest.Requirement.ItemType,quest.Requirement.Amount)) return;

            player.Money += quest.Reward;
            
            //TODO add disabling of arrows
            quest.Target.HighlightObject.SetActive(false);
            quest.Target.IsBusy = false;
            activeQuests.Remove(quest);
        }

        #endregion
        
    }

}