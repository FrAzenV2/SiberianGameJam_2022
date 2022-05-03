using System.Collections;
using Common.Scripts.Enums;
using Common.Scripts.ScriptableObjects;
using UnityEngine;

namespace Common.Scripts.Components
{
    public class ItemEntityComponent : MonoBehaviour
    {
        [field: SerializeField] public ItemConfig ItemConfig { get; private set; }
        [field: SerializeField] public Renderer Renderer { get; private set; }
        public void StartSelfDestruction()
        {
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