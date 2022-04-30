using UnityEngine;

namespace Components
{
    public class PlayerEnitityComponent : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
    }
}