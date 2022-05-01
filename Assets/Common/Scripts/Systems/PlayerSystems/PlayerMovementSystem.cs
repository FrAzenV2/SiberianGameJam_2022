using System.Collections;
using Common.Scripts.ScriptableObjects;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems.PlayerSystems
{
    public class PlayerMovementSystem : GameSystem
    {


        private const float GravityStanding = -0.5f;
        private const float GravityFalling = -10;

        private Camera playerCamera;
        private MovementConfig movementConfig;

        private float currentSpeedModifier = 0;
        private float currentRunModifier = 1;

        private Vector3 smoothVelocity;

        public override void OnInit()
        {
            playerCamera = Camera.main;
        }

        public override void OnUpdate()
        {
            Accelerate();
            TryAddRunDash();
            Rotate();
            Move();
            SimulateGravity();

            UpdateCurrentData();
        }

        private void UpdateCurrentData()
        {
            var playerEntityTransform = game.PlayerEntity.transform;
            game.CurrentForward = playerEntityTransform.forward;
            game.CurrentRotation = playerEntityTransform.rotation;
            game.CurrentSpeed = movementConfig.MaxMoveSpeed * currentRunModifier * currentSpeedModifier;
            game.CurrentSpeedPercentage = currentRunModifier * currentSpeedModifier;
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
                currentRunModifier = Mathf.Clamp(currentRunModifier + movementConfig.RunCoefficientAccelerateStep,
                    1, movementConfig.MaxRunCoefficient);

            if (game.CurrentMovementInput == Vector3.zero) currentRunModifier = 1;

            game.RunNextFrame = false;
        }

        private void Rotate()
        {
            if (game.CurrentMovementInput == Vector3.zero) return;

            var toRotation = Quaternion.LookRotation(game.CurrentMovementInput, Vector3.up);

            var rotationEulerAngles = toRotation.eulerAngles;
            rotationEulerAngles.y += playerCamera.transform.eulerAngles.y;
            toRotation.eulerAngles = rotationEulerAngles;

            var currentDeltaRotation = Quaternion.RotateTowards(game.PlayerEntity.transform.rotation, toRotation,
                movementConfig.TurnSpeed * Time.deltaTime);

            game.CurrentDeltaRotationEulers = currentDeltaRotation.eulerAngles;

            game.PlayerEntity.transform.rotation = currentDeltaRotation;
        }

        private void Move()
        {
            if (game.CurrentMovementInput == Vector3.zero) return;

            game.PlayerEntity.CharacterController.Move(game.PlayerEntity.transform.forward *
                (movementConfig.MaxMoveSpeed *
                    currentRunModifier *
                    currentSpeedModifier *
                    Time.deltaTime));

        }

        private void SimulateGravity()
        {
            game.PlayerEntity.CharacterController.Move(Vector3.up *
                (game.PlayerEntity.CharacterController.isGrounded ? GravityStanding : GravityFalling) *
                Time.deltaTime);
        }

    }
}