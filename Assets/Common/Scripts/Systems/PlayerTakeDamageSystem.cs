using Common.Scripts.Components;
using Common.Scripts.ScriptableObjects;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class PlayerTakeDamageSystem : GameSystem
    {
        private PlayerDamageSystemConfig systemConfig;

        public override void OnStateEnter()
        {
            game.PlayerEntity.TakeDamageTrigger.OnTriggerEntered += TryTakeDamage;
        }
        public override void OnStateExit()
        {
            game.PlayerEntity.TakeDamageTrigger.OnTriggerEntered -= TryTakeDamage;
        }
        
        private void TryTakeDamage(GameObject obj)
        {
            if(!obj.TryGetComponent(out DamageDealComponent damageDealComponent)) return;

            if(obj.CompareTag("Chaser"))
                TakeDamageFromChaser(obj);
            
            if (obj.CompareTag("SpeedObstacle"))
                TakeDamageFromBump();
        }
        private void TakeDamageFromBump()
        {
            if(game.CurrentSpeed < systemConfig.SpeedToTakeBumpDamage) return;
            
            Bootstrap.GetSystem<StackSystem>().ReleaseItems(systemConfig.LoseItemsOnBump);
        }

        private void TakeDamageFromChaser(GameObject obj)
        {
            obj.GetComponent<ChaserEntityComponent>().StartSelfDestruction();
            Bootstrap.GetSystem<StackSystem>().ReleaseItems(systemConfig.LoseItemsAmountOnCatch);
        }
    }
}