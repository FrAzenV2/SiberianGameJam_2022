using System.Collections;
using System.Linq;
using Common.Scripts.ScriptableObjects;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class StackSystem : GameSystem
    {
        private StackSystemConfig stackConfig;
        private MovementConfig movementConfig;

        public override void OnInit()
        {
            StartCoroutine(UpdateForward());
        }
        public override void OnUpdate()
        {
            MoveStacks();
        }

        [Button()]
        private void DebugSpawnItem()
        {
            var newItemHeight = game.StackList.Sum(itemEntityComponent => itemEntityComponent.ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems);

            var item = Instantiate(stackConfig.DefaultItem, game.PlayerEntity.StackingParent);
            game.StackList.Add(item);
            item.transform.localPosition = Vector3.up*newItemHeight;
        }
        
        private void MoveStacks()
        {
            if(game.StackList.Count < 2 ) return;
            
            var baseLocalPos = game.StackList[0].transform.localPosition;

            var speedModifier = Mathf.InverseLerp(0,movementConfig.MaxRunCoefficient,game.CurrentSpeedPercentage);
            var currentHeight = game.StackList[0].ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems;
            for (var i = 1; i < game.StackList.Count; i++)
            {
                var currentForward = game.StackList[i].transform.InverseTransformDirection(game.PreviousForward);
                
                var newLocalPos = Vector3.MoveTowards(game.StackList[i].transform.localPosition,
                    baseLocalPos + Vector3.up*currentHeight - currentForward * (speedModifier*stackConfig.OffsetStep*i),
                    stackConfig.StackMovementSpeed*Time.deltaTime);
                game.StackList[i].transform.localPosition = newLocalPos;
                currentHeight += game.StackList[i].ItemConfig.ItemHeight + stackConfig.SpaceBetweenItems;
            }
        }
        
        
        private IEnumerator UpdateForward()
        {
            var waitFor = new WaitForSeconds(stackConfig.PreviousForwardUpdateRate);
            while (true)
            {
                game.PreviousForward = game.PlayerEntity.transform.forward;
                yield return waitFor;
            }
        }
    }
}