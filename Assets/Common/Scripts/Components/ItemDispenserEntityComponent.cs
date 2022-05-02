using Common.Scripts.ScriptableObjects;
using UnityEngine;

namespace Common.Scripts.Components
{
    public class ItemDispenserEntityComponent : MonoBehaviour
    {
        [field: Tooltip("Seconds Per Item")][field: SerializeField] public float DispenseRate { get; private set; } = 1;
        [field: SerializeField] public ItemConfig ItemConfig { get; private set;}
    }
}