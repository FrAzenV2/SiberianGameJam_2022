using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.Components
{
    public class UpgradeBarElement : MonoBehaviour
    {
        [field: SerializeField] public Button UpgradeButton { get; private set; }

        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Image[] upgradePoints;
        [SerializeField] private Sprite activePoint;
        [SerializeField] private Sprite inactivePoint;
        [SerializeField] private TMP_Text upgradeNameText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private string costPrefix;

        public void SetData(string upgradeName,Sprite upgradeSprite, int upgradeLevel, int nextLevelCost)
        {
            upgradeNameText.text = upgradeName;
            upgradeIcon.overrideSprite = upgradeSprite;
            
            for (int i = 0; i < upgradeLevel; i++)
            {
                upgradePoints[i].overrideSprite = activePoint;
            }
            
            costText.text = costPrefix + nextLevelCost;
            if (nextLevelCost == -1)
                costText.text = "MAX";
        }

        public void UpdateData(int upgradeLevel, int nextLevelCost)
        {
            for (int i = 0; i < upgradeLevel; i++)
            {
                upgradePoints[i].overrideSprite = activePoint;
            }
            
            if (nextLevelCost == -1)
                costText.text = "MAX";
        }
    }
}