using Kuhpik;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Scripts.Systems
{
    public class InputReaderSystem : GameSystem
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            game.CurrentMovementInput = new Vector3(value.x,0,value.y).normalized;
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            game.RunNextFrame = true;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            game.InteractNextFrame = true;
        }
    }
}
