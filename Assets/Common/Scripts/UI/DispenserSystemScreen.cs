using System;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class DispenserSystemScreen : UIScreen
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool isTextColorful = true;
        [SerializeField] private Image pickupProgressBar;
        [SerializeField] private Image inventoryBar;
        [SerializeField] private ColorGrading[] colors;

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

        public void UpdateInventoryBar(int current,int max)
        {
            text.text = current + "|" + max;
            
            var percentage = (float) current / max;
            inventoryBar.fillAmount = percentage;
            
            var currentColor = Color.white;
            
            foreach (var colorGrading in colors)
            {
                if (colorGrading.Percentage <= percentage)
                    currentColor = colorGrading.Color;
            }
            
            inventoryBar.color = currentColor;
            if(isTextColorful)
                text.color = currentColor;
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
            if(!currentTarget) return;
            
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