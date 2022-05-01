using UnityEngine;

namespace Common.Scripts.Components
{
    public class PlayerEntityComponent : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
    }
}