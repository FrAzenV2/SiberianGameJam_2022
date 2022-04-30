using Common.Scripts.ScriptableObjects;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class PlayerMovementSystem : GameSystem
    {
        private Camera playerCamera;
        private MovementConfig movementConfig;
            
        private float currentSpeedModifier = 0;
        private float currentRunModifier = 1;

        private Vector3 smoothVelocity;

        public override void OnInit()
        {
            playerCamera =Camera.main;
        }

        public override void OnUpdate()
        {
            Accelerate();
            TryAddRunDash();
            Rotate();
            
        }

        public override void OnFixedUpdate()
        {
            Move();
        }

        private void Accelerate()
        {
            currentSpeedModifier = Mathf.Clamp01(currentSpeedModifier + movementConfig.MovementAccelerationStep * Time.deltaTime);
            if (game.CurrentMovementInput == Vector3.zero)
                currentSpeedModifier = 0;
        }

        private void TryAddRunDash()
        {
            currentRunModifier = Mathf.Clamp(currentRunModifier - movementConfig.RunCoefficientDecelerateStep * Time.deltaTime,
                1, movementConfig.MaxRunCoefficient);
            
            if (game.RunNextFrame)
            {
                currentRunModifier = Mathf.Clamp(currentRunModifier + movementConfig.RunCoefficientAccelerateStep,
                    1, movementConfig.MaxRunCoefficient);
            }

            if (game.CurrentMovementInput == Vector3.zero) currentRunModifier = 1;

            game.RunNextFrame = false;
        }

        private void Rotate()
        {
            if(game.CurrentMovementInput==Vector3.zero) return;
            
            var toRotation = Quaternion.LookRotation(game.CurrentMovementInput, Vector3.up);

            
            var rotationEulerAngles = toRotation.eulerAngles;
            rotationEulerAngles.y += playerCamera.transform.eulerAngles.y;
            toRotation.eulerAngles = rotationEulerAngles;
            
            game.PlayerEntity.transform.rotation = Quaternion.RotateTowards(game.PlayerEntity.transform.rotation, toRotation,
                movementConfig.TurnSpeed * Time.deltaTime); 
        }
        
        private void Move()
        {
            if (game.CurrentMovementInput == Vector3.zero)
            {
                var newVelocity = game.PlayerEntity.Rigidbody.velocity;
                newVelocity = Vector3.Lerp(Vector3.zero, newVelocity, 0.9f);
                
                if (newVelocity.magnitude < 2f) newVelocity = Vector3.zero;
                
                newVelocity.y = game.PlayerEntity.Rigidbody.velocity.y;

                game.PlayerEntity.Rigidbody.velocity = newVelocity;
                
                return;
            }

            var velocity = game.PlayerEntity.Rigidbody.velocity;
            game.PlayerEntity.Rigidbody.velocity = Vector3.SmoothDamp(velocity,
                game.PlayerEntity.transform.forward*movementConfig.MaxMoveSpeed*
                currentSpeedModifier*currentRunModifier*Time.fixedDeltaTime + Vector3.up * velocity.y,
                ref smoothVelocity,
                0.4f);

/*var horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
if(horizontalVelocity.magnitude < movementConfig.MaxMoveSpeed)
    game.PlayerEntity.Rigidbody.AddForce(game.PlayerEntity.transform.forward*movementConfig.MaxMoveSpeed*
        currentSpeedModifier*currentRunModifier*Time.fixedDeltaTime, ForceMode.Force);*/
}

}
}