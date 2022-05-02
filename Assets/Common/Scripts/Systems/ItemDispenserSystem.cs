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
        private bool isDispensing;
        private Coroutine currentRoutine;
        public override void OnInit()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered += OnTryToInteract;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited += OnTryToStopInteraction;
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
                Bootstrap.GetSystem<StackSystem>().AddItem(dispenser.ItemConfig);
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
        
    }
}