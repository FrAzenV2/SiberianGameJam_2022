using Common.Scripts.Components;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Puddle System Config", menuName = "Configs/Systems/Puddle Config", order = 0)]
    public class PuddleSystemConfig : ScriptableObject
    {
        [field: SerializeField] public float DirtResetTime { get; private set; } = 15f;
        
        [field: SerializeField] public float ChasersSpeed { get; private set; } = 10;
        [field: SerializeField] public int ChasersAmount { get; private set; } = 2;
        [field: SerializeField] public ChaserEntityComponent ChaserPrefab { get; private set;}
    }
}