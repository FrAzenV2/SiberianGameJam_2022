using Common.Scripts.Enums;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Delivery Requirement", menuName = "Configs/Quests/Delivery Requirement", order = 0)]
    public class DeliveryRequirement : ScriptableObject
    {
        [field: SerializeField] public ItemConfig ItemConfig { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
    }
}