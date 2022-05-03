using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Common.Scripts.Components
{
    public class ChaserEntityComponent : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public Renderer Renderer { get; private set; }
        public Transform CurrentTarget { get; set; }

        public event Action<ChaserEntityComponent> OnSelfDestructionStarted;
        public async void StartSelfDestruction()
        {
            await Task.Yield();
            OnSelfDestructionStarted?.Invoke(this);
            NavMeshAgent.SetDestination(Vector3.zero);
            StartCoroutine(SelfDestructionCoroutine());
        }

        private IEnumerator SelfDestructionCoroutine()
        {
            while (Renderer.isVisible)
            {
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}