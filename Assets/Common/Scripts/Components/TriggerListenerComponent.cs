using System;
using UnityEngine;

namespace Common.Scripts.Components
{
    public class TriggerListenerComponent : MonoBehaviour
    {
        public event Action<GameObject> OnTriggerEntered;
        public event Action<GameObject> OnTriggerExited;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other.gameObject);
        }
        private void OnTriggerExit(Collider other)
        {
            OnTriggerExited?.Invoke(other.gameObject);
        }
    }
}