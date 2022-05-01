using System.Linq;
using Common.Scripts.Components;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class StackSystem : GameSystem
    {
        [SerializeField] private float offsetStep = 0.2f;
        [SerializeField] private float spaceBetweenItems = 0.1f;
        [SerializeField] private float stackMovementSpeed = 0.4f;

        [SerializeField] private ItemEntityComponent Item;
        public override void OnUpdate()
        {
            MoveStacks();
        }

        [Button()]
        private void DebugSpawnItem()
        {
            var newItemHeight = game.StackList.Sum(itemEntityComponent => itemEntityComponent.ItemConfig.ItemHeight + spaceBetweenItems);

            var item = Instantiate(Item, game.PlayerEntity.StackingParent);
            game.StackList.Add(item);
            item.transform.localPosition = Vector3.up*newItemHeight;
        }
        
        private void MoveStacks()
        {
            if(game.StackList.Count < 2 ) return;
            
            var baseLocalPos = game.StackList[0].transform.localPosition;
            //TODO change from hardcoded value to config paramenter;
            var speedModifier = Mathf.InverseLerp(0,2.5f,game.CurrentSpeedPercentage);
            var currentHeight = game.StackList[0].ItemConfig.ItemHeight + spaceBetweenItems;
            for (var i = 1; i < game.StackList.Count; i++)
            {
                var currentForward = game.StackList[i].transform.InverseTransformDirection(game.PreviousForward);
                
                var newLocalPos = Vector3.MoveTowards(game.StackList[i].transform.localPosition,
                    baseLocalPos + Vector3.up*currentHeight - currentForward * (speedModifier*offsetStep*i),
                    stackMovementSpeed*Time.deltaTime);
                game.StackList[i].transform.localPosition = newLocalPos;
                currentHeight += game.StackList[i].ItemConfig.ItemHeight + spaceBetweenItems;
            }
        }
    }
}