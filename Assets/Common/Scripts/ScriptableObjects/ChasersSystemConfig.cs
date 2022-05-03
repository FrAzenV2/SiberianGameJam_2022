using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Chasers System Config", menuName = "Configs/Systems/Chasers System Config", order = 0)]
    public class ChasersSystemConfig : ScriptableObject
    {
        [field: Tooltip("Updates per second")][field: SerializeField] public float UpdateRate { get; private set; } = 10;
    }
}