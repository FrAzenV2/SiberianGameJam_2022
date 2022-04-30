using UnityEngine;
using NaughtyAttributes;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Kuhpik/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        // Example
        // [SerializeField] [BoxGroup("Moving")] private float moveSpeed;
        // public float MoveSpeed => moveSpeed;

        [BoxGroup("Movement")] [SerializeField] private float maxMoveSpeed = 10;
        [BoxGroup("Movement")] [SerializeField] private float additionalRunSpeed = 15;
        [BoxGroup("Movement")] [SerializeField] private float turnSpeed = 90;
        
    }
}