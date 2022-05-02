using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.Components
{
    public class ItemIconComponent : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text amountText;

        public void Init(Sprite itemSprite, int amount)
        {
            itemIcon.sprite = itemSprite;
            amountText.text = amount.ToString();
        }
    }
}