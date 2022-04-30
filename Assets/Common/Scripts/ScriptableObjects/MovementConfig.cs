using NaughtyAttributes;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Movement Config", menuName = "Configs/Movement Config", order = 0)]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxMoveSpeed { get; private set; } = 10;
        [field: SerializeField] public float MovementAccelerationStep { get; private set; } = 0.55f;
        [field: SerializeField] public float MaxRunCoefficient { get; private set; } = 3;
        [field: Tooltip("Acceleration Coefficient is added to speed")][field: SerializeField] public float RunCoefficientAccelerateStep { get; private set; } = 0.15f;
        [field: SerializeField] public float RunCoefficientDecelerateStep { get; private set; } = 0.5f;
        [field: Tooltip("In angles)")][field: SerializeField] public float TurnSpeed { get; private set; } = 90;
    }
}