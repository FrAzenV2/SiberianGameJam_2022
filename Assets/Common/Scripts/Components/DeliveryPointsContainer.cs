using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Scripts.Components
{
    public class DeliveryPointsContainer : MonoBehaviour
    {
        public List<DeliveryPointComponent> DeliveryPoints { get; private set; }
        private void Awake()
        {
            DeliveryPoints = GetComponentsInChildren<DeliveryPointComponent>().ToList();
        }
    }
}