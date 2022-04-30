using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Movement Config", menuName = "Configs/Movement Config", order = 0)]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxMoveSpeed { get; private set; } = 10;
        [field: SerializeField] public float AdditionalRunSpeed { get; private set; } = 15;
        [field: SerializeField] public float TurnSpeed { get; private set; } = 90;
    }
}