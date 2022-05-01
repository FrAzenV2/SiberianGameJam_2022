using System;
using System.Collections;
using System.Linq;
using Common.Scripts.Components;
using Common.Scripts.ScriptableObjects;
using Common.Scripts.UI;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class ItemDispenserSystem : GameSystemWithScreen<DispenserSystemScreen>
    {
        private StackSystemConfig stackSystemConfig;
        private bool isDispensing;
        private Coroutine currentRoutine;
        public override void OnInit()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered += OnTryToInteract;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited += OnTryToStopInteraction;

            screen.UpdateInventoryBar(game.StackList.Count, game.CurrentStackMax);
        }

        private void OnDisable()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered -= OnTryToInteract;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited -= OnTryToStopInteraction;
        }

        private void OnTryToStopInteraction(GameObject obj)
        {
            if (!obj.TryGetComponent(out ItemDispenserEntityComponent dispenserEntityComponent)) return;

            screen.HideIndicator();
            if (isDispensing)
                StopInteraction();
        }
        private void OnTryToInteract(GameObject obj)
        {
            if (!obj.TryGetComponent(out ItemDispenserEntityComponent dispenserEntityComponent)) return;

            StartDispensing(dispenserEntityComponent);
        }

        private void StopInteraction()
        {
            StopCoroutine(currentRoutine);
        }

        private void StartDispensing(ItemDispenserEntityComponent dispenserEntityComponent)
        {
            screen.ShowIndicator();
            currentRoutine = StartCoroutine(DispenserRoutine(dispenserEntityComponent));
        }

        private IEnumerator DispenserRoutine(ItemDispenserEntityComponent dispenser)
        {
            isDispensing = true;

            while (game.StackList.Count < game.CurrentStackMax)
            {
                yield return DispensingProcess(dispenser.DispenseRate);
                SpawnNewItem(dispenser.ItemConfig);
                screen.UpdateInventoryBar(game.StackList.Count, game.CurrentStackMax);
            }

            screen.HideIndicator();
            isDispensing = false;
        }

        private IEnumerator DispensingProcess(float time)
        {
            float timer = 0;
            while (time > timer)
            {
                timer += Time.deltaTime;
                screen.UpdateDispensingProcess(timer, time);
                yield return null;
            }
        }

        private ItemEntityComponent SpawnNewItem(ItemConfig itemConfig)
        {
            var newItemHeight = game.StackList.Sum(itemEntityComponent => itemEntityComponent.ItemConfig.ItemHeight + stackSystemConfig.SpaceBetweenItems);

            var item = Instantiate(itemConfig.ItemPrefab, game.PlayerEntity.StackingParent);

            game.StackList.Add(item);
            item.transform.localPosition = Vector3.up * newItemHeight;

            return item;
        }
    }
}