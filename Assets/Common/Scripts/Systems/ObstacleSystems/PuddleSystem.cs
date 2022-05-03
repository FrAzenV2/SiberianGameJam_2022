using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Components;
using Common.Scripts.ScriptableObjects;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems.ObstacleSystems
{
    public class PuddleSystem : GameSystem
    {
        private PuddleSystemConfig puddleSystemConfig;

        private List<ChaserEntityComponent> currentChasers = new List<ChaserEntityComponent>();

        private float timer;
        
        private Coroutine dirtRemoveCoroutine;

        public override void OnStateEnter()
        {
            game.PlayerEntity.PuddleTrigger.OnTriggerEntered += TryTouchPuddle;
        }

        public override void OnStateExit()
        {
            game.PlayerEntity.PuddleTrigger.OnTriggerEntered -= TryTouchPuddle;
        }
        
        private void TryTouchPuddle(GameObject obj)
        {
            if(!obj.TryGetComponent(out PuddleEntityComponent puddle)) return;

            TouchPuddle(puddle);
        }
        private void TouchPuddle(PuddleEntityComponent component)
        {
            //todo add effects on boar
            if (component.DirtyPuddle)
            {
                timer = puddleSystemConfig.DirtResetTime;
                dirtRemoveCoroutine ??= StartCoroutine(DirtResetCoroutine());
                
                if(!game.IsDirty)
                    StartChasingPlayer(component);
            }
            else if (dirtRemoveCoroutine != null)
            {
                StopCoroutine(dirtRemoveCoroutine);
                StopChasingPlayer();
            }
            
            game.IsDirty = component.DirtyPuddle;
        }
        private void StartChasingPlayer(PuddleEntityComponent puddleEntityComponent)
        {
            currentChasers = Bootstrap.GetSystem<ChasersSystem>().AddChasers(puddleSystemConfig.ChaserPrefab, puddleSystemConfig.ChasersAmount);
            foreach (var chaser in currentChasers)
            {
                chaser.transform.position = puddleEntityComponent.ChasersSpawnPoint[Random.Range(0, puddleEntityComponent.ChasersSpawnPoint.Length)].position;
                chaser.CurrentTarget = game.PlayerEntity.transform;
                chaser.NavMeshAgent.speed = puddleSystemConfig.ChasersSpeed;
                chaser.OnSelfDestructionStarted += RemoveFromCurrentChasers;
            }
        }
        private void RemoveFromCurrentChasers(ChaserEntityComponent chaser)
        {
            chaser.OnSelfDestructionStarted -= RemoveFromCurrentChasers;
            currentChasers.Remove(chaser);
        }
        
        private void StopChasingPlayer()
        {
            foreach (var chaser in currentChasers)
            {
                chaser.StartSelfDestruction();
            }
        }
        
        private IEnumerator DirtResetCoroutine()
        {
            while (timer>0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            //todo add effects on boar
            game.IsDirty = false;
            StopChasingPlayer();
        }

    }
}