using Kuhpik;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class InputReaderComponent : GameSystem
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            game.CurrentMovementInput = value;
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            game.RunNextFrame = true;
        }
    }
}
