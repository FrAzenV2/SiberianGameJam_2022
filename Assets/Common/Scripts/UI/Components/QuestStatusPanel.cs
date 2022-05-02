using Common.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.Components
{
    public class QuestStatusPanel : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image timerBar;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private ItemIconComponent itemIconPrefab;
        [SerializeField] private ColorGrading[] colorGradings;

        public Color Color;
        public void Init(Sprite itemIcon, int amount, Color backgroundColor)
        {
            var item = Instantiate(itemIconPrefab, itemsParent);
            item.Init(itemIcon,amount);
            Color = backgroundColor;
            backgroundImage.color = backgroundColor == null ? Color.white : backgroundColor;
            timerBar.fillAmount = 1;
        }
        
        public void UpdateTimer(float percentage)
        {
            timerBar.fillAmount = percentage;

            var currentColor = Color.white;

            foreach (var colorGrading in colorGradings)
                if (colorGrading.Percentage < percentage)
                    currentColor = colorGrading.Color;

            timerBar.color = currentColor;
        }
        
    }
}