using Common.Scripts.Components;
using Common.Scripts.Enums;
using NaughtyAttributes;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New ItemConfig", menuName = "Configs/Item config", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public ItemEntityComponent ItemPrefab { get; private set; }
        [field: ShowAssetPreview][field: SerializeField] public Sprite ItemIcon { get; private set; }
        [field: SerializeField] public float ItemHeight { get; private set; }
    }
}