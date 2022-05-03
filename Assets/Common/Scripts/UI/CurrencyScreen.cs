using DG.Tweening;
using Kuhpik;
using TMPro;
using UnityEngine;

namespace Common.Scripts.UI
{
    public class CurrencyScreen : UIScreen
    {
        [SerializeField] private TMP_Text moneyText;
        private float currentMoney;
        public override void Open()
        {
            base.Open();
            Bootstrap.PlayerData.MoneyChanged += UpdateMoney;
            UpdateMoney();
        }
        private void UpdateMoney()
        {
            DOTween.To(() => currentMoney, x => currentMoney = x,endValue: Bootstrap.PlayerData.Money, 0.3f)
                .OnUpdate(() =>
                {
                    moneyText.text = ((int)currentMoney).ToString();
                });
        }
    }
}