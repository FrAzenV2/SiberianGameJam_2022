using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Components;
using Common.Scripts.ScriptableObjects;
using JetBrains.Annotations;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class ChasersSystem : GameSystem
    {
        [SerializeField] private Transform chasersParent = null;

        private ChasersSystemConfig chasersSystemConfig;

        private Coroutine chasersUpdateCoroutine;
        public override void OnStateEnter()
        {
            chasersUpdateCoroutine = StartCoroutine(UpdateChasersDestination(chasersSystemConfig.UpdateRate));
        }

        public override void OnStateExit()
        {
            StopCoroutine(chasersUpdateCoroutine);
        }
        
        public List<ChaserEntityComponent> AddChasers(ChaserEntityComponent prefab, int amount)
        {
            var newChasers = new List<ChaserEntityComponent>();

            for (var i = 0; i < amount; i++)
            {
                var newChaser = Instantiate(prefab, chasersParent);
                game.ActiveChasers.Add(newChaser);
                newChasers.Add(newChaser);
                newChaser.OnSelfDestructionStarted += RemoveChaser;
            }
            return newChasers;
        }
        
        private IEnumerator UpdateChasersDestination(float rate)
        {
            while (true)
            {
                foreach (var chaser in game.ActiveChasers)
                {
                    if(chaser.CurrentTarget==null) continue;
                    
                    chaser.NavMeshAgent.SetDestination(chaser.CurrentTarget.position);
                }
                yield return new WaitForSeconds(1/rate);
            }
        }
        
        public void RemoveChasers(List<ChaserEntityComponent> chasers)
        {
            game.ActiveChasers.RemoveAll(chasers.Contains);

            foreach (var chaser in chasers)
            {
                chaser.StartSelfDestruction();
            }
        }

        private void RemoveChaser(ChaserEntityComponent chaser)
        {
            chaser.OnSelfDestructionStarted -= RemoveChaser;
            game.ActiveChasers.Remove(chaser);
        }
    }
}