using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Damage System Config", menuName = "Configs/Systems/Player Damage System", order = 0)]
    public class PlayerDamageSystemConfig : ScriptableObject
    {
        [field: SerializeField] public int LoseItemsAmountOnCatch { get; private set; } = 1;
        [field: SerializeField] public int LoseItemsOnBump { get; private set; } = 1;
        [field: SerializeField] public float SpeedToTakeBumpDamage { get; private set; } = 7;
    }
}