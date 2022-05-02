using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI
{
    public class StackSystemScreen : UIScreen
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool isTextColorful = true;
        [SerializeField] private Image inventoryBar;
        [SerializeField] private ColorGrading[] colors;
        
        public void UpdateInventoryBar(int current, int max)
        {
            text.text = current + "|" + max;

            var percentage = (float)current / max;
            inventoryBar.fillAmount = percentage;

            var currentColor = Color.white;

            foreach (var colorGrading in colors)
                if (colorGrading.Percentage <= percentage)
                    currentColor = colorGrading.Color;

            inventoryBar.color = currentColor;
            if (isTextColorful)
                text.color = currentColor;
        }
    }
}