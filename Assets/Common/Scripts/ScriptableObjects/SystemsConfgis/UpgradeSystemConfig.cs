using Common.Scripts.ScriptableObjects.UpgradeConfigs;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Upgrade System Config", menuName = "Configs/Systems/Upgrade System", order = 0)]
    public class UpgradeSystemConfig : ScriptableObject
    {
        [field: SerializeField] public UpgradeConfig SpeedUpgradeConfig { get; private set; }
        [field: SerializeField] public UpgradeConfig TurnUpgradeConfig { get; private set; }
        [field: SerializeField] public UpgradeConfig RewardUpgradeConfig { get; private set; }
    }
}