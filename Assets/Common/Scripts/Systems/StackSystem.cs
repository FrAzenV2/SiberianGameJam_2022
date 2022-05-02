using System.Collections;
using System.Linq;
using Common.Scripts.Enums;
using Common.Scripts.ScriptableObjects;
using Common.Scripts.UI;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class StackSystem : GameSystemWithScreen<StackSystemScreen>
    {
        private StackSystemConfig stackConfig;
        private MovementConfig movementConfig;

        public override void OnInit()
        {
            game.DefaultStackMax = stackConfig.MaxObjects;
            StartCoroutine(UpdateForward());
            
            screen.UpdateInventoryBar(game.StackList.Count, game.CurrentStackMax);
        }

        public override void OnFixedUpdate()
        {
            MoveStacks();
        }

        public void AddItem(ItemConfig itemConfig)
        {
            var newItemHeight = game.StackList.Sum(itemEntityComponent => itemEntityComponent.ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems);

            var item = Instantiate(itemConfig.ItemPrefab, game.PlayerEntity.StackingParent);

            var itemType = item.ItemConfig.ItemType;
            if (!game.StackItems.ContainsKey(itemType))
                game.StackItems[itemType] = 0;
            game.StackItems[itemType]++;
            
            game.StackList.Add(item);
            item.transform.localPosition = Vector3.up * newItemHeight;
            screen.UpdateInventoryBar(game.StackList.Count, game.CurrentStackMax);
        }

        public bool RemoveItem(ItemType itemType, int amount = 1)
        {
            if (game.StackItems[itemType] < amount) return false;

            game.StackItems[itemType] -= amount;

            for (var i = 0; i < amount; i++)
            {
                var item = game.StackList.FindLast(item => item.ItemConfig.ItemType == itemType);
                game.StackList.Remove(item);
                Destroy(item.gameObject);
            }

            return true;
        }
        
        

        [Button()]
        private void DebugSpawnItem()
        {
            var newItemHeight = game.StackList.Sum(itemEntityComponent => itemEntityComponent.ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems);

            var item = Instantiate(stackConfig.DefaultItem, game.PlayerEntity.StackingParent);
            game.StackList.Add(item);
            item.transform.localPosition = Vector3.up * newItemHeight;
        }

        private void MoveStacks()
        {
            if (game.StackList.Count < 2) return;

            var baseLocalPos = game.StackList[0].transform.localPosition;

            var speedModifier = Mathf.InverseLerp(0, movementConfig.MaxRunCoefficient, game.CurrentSpeedPercentage);
            var currentHeight = game.StackList[0].ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems;
            for (var i = 1; i < game.StackList.Count; i++)
            {
                var currentForward = game.StackList[i].transform.InverseTransformDirection(game.PreviousForward);

                var newLocalPos = Vector3.MoveTowards(game.StackList[i].transform.localPosition,
                    baseLocalPos + Vector3.up * currentHeight - currentForward * (speedModifier * stackConfig.OffsetStep * i),
                    stackConfig.StackMovementSpeed * Time.deltaTime);
                game.StackList[i].transform.localPosition = newLocalPos;
                currentHeight += game.StackList[i].ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems;
            }
        }


        private IEnumerator UpdateForward()
        {
            while (true)
            {
                game.PreviousForward = game.PlayerEntity.transform.forward;
                //TODO optimize by drawing init of waitFor In start of Coroutine
                yield return new WaitForSeconds(stackConfig.PreviousForwardUpdateRate);
            }
        }
    }
}