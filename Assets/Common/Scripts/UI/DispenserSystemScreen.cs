using System;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class DispenserSystemScreen : UIScreen
    {
        [SerializeField] private Image pickupProgressBar;
        
        private Transform currentTarget;
        private Camera playerCamera;

        public override void Open()
        {
            base.Open();

            playerCamera = Camera.main;
            SetTarget(Bootstrap.GameData.PlayerEntity.IndicatorPosition);
        }

        public void UpdateDispensingProcess(float timer, float time)
        {
            pickupProgressBar.fillAmount = timer / time;
        }

        private void SetTarget(Transform target)
        {
            currentTarget = target;
        }
        public void ShowIndicator()
        {
            pickupProgressBar.gameObject.SetActive(true);
        }
        public void HideIndicator()
        {
            pickupProgressBar.gameObject.SetActive(false);
        }

        private void MoveIndicator(Vector3 position)
        {
            pickupProgressBar.transform.position = position;
        }

        private void LateUpdate()
        {
            if (!currentTarget) return;

            MoveIndicator(playerCamera.WorldToScreenPoint(currentTarget.position));
        }
    }

    [Serializable]
    public sealed class ColorGrading
    {
        public Color Color;
        public float Percentage;
    }
}