using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Delivery System Config", menuName = "Configs/Systems/Delivery Config", order = 0)]
    public class DeliverySystemConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxQuests { get; private set; } = 3;
        [field: SerializeField] public float SecondsPerMeter { get; private set; } = .4f;
        [field: SerializeField] public float RewardPerMeter { get; private set; } = .1f;
        [field: SerializeField] public DeliveryRequirement[] DeliveryRequirements { get; private set; }
    }
}