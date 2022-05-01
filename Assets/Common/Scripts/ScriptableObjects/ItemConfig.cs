using NaughtyAttributes;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New ItemConfig", menuName = "Configs/Item config", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject ItemPrefab { get; private set; }
        [field: ShowAssetPreview][field: SerializeField] public Sprite ItemIcon { get; private set; }
        [field: SerializeField] public float ItemHeight { get; private set; }
    }
}