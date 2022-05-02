using System;
using UnityEngine;

namespace Common.Scripts.Components
{
    public class DeliveryPointComponent : MonoBehaviour
    {
        [field: SerializeField] public Transform TargetPosition { get; private set; }
        [field: SerializeField] public GameObject HighlightObject { get; private set; }
        public bool IsBusy { get; set; }

        private void Awake()
        {
            HighlightObject.SetActive(false);
        }
    }
}