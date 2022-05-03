using Common.Scripts.Components;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Stack System Config", menuName = "Configs/Systems/Stack system", order = 0)]
    public class StackSystemConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxObjects { get; private set; } = 6;
        [field: SerializeField] public float OffsetStep { get; private set; } = 0.2f;
        [field: SerializeField] public float SpaceBetweenItems { get; private set; } = 0.1f;
        [field: SerializeField] public float StackMovementSpeed { get; private set; } = 0.4f;
        [field: SerializeField] public ItemEntityComponent DefaultItem { get; private set; }
        [field: SerializeField] public float PreviousForwardUpdateRate { get; private set; } = .3f;
    }
}