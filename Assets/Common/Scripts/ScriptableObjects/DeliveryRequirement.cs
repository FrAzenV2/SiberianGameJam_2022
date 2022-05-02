using Common.Scripts.Enums;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Delivery Requirement", menuName = "Configs/Delivery Requirement", order = 0)]
    public class DeliveryRequirement : ScriptableObject
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
    }
}