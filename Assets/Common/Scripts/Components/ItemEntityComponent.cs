using Common.Scripts.Enums;
using Common.Scripts.ScriptableObjects;
using UnityEngine;

namespace Common.Scripts.Components
{
    public class ItemEntityComponent : MonoBehaviour
    {
        [field: SerializeField] public ItemConfig ItemConfig { get; private set; }
    }
}