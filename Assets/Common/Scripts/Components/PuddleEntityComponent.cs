using UnityEngine;

namespace Common.Scripts.Components
{
    public class PuddleEntityComponent : MonoBehaviour
    {
        [field: SerializeField] public bool DirtyPuddle { get; private set; } = true;
        [field: SerializeField] public Transform[] ChasersSpawnPoint { get; private set; }
    }
}