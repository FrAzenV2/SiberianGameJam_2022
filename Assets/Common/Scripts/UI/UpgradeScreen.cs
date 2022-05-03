using System;
using System.Collections.Generic;
using Common.Scripts.ScriptableObjects.UpgradeConfigs;
using Common.Scripts.UI.Components;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.UI
{
    public class UpgradeScreen : UIScreen
    {
        [SerializeField] private Transform panelParent;
        [SerializeField] private UpgradeBarElement[] upgradeBarElements;
        [SerializeField] private float hideShowTime = 0.4f;
        [SerializeField] private Transform showPosition;
        [SerializeField] private Transform hidePosition;

        private Dictionary<UpgradeType, UpgradeBarElement> upgradeBarElementsDictionary = new Dictionary<UpgradeType, UpgradeBarElement>();

        public event Action<UpgradeType> OnTryToUpgrade;
        public override void Open()
        {
            panelParent.position = hidePosition.position;
            base.Open();
            DOTween.Rewind(this);
            panelParent.DOMove(showPosition.position, hideShowTime);
        }

        public override void Close()
        {
            DOTween.Rewind(this);
            panelParent.DOMove(hidePosition.position, hideShowTime).OnComplete(()=> base.Close());
        }

        private void OnDisable()
        {
            UnsubscribeFromButtons();
        }

        private void SubscribeToButtons()
        {
            foreach (var pair in upgradeBarElementsDictionary)
            {
                pair.Value.UpgradeButton.onClick.AddListener(() => OnTryToUpgrade?.Invoke(pair.Key));
            }   
        }

        private void UnsubscribeFromButtons()
        {
            foreach (var pair in upgradeBarElementsDictionary)
            {
                pair.Value.UpgradeButton.onClick.RemoveAllListeners();
            }   
        }

        public void SetupScreen(UpgradeConfig[] upgradeConfigs, int[] levels)
        {
            for (var i = 0; i < upgradeBarElements.Length; i++)
            {
                var upgradeConfig = upgradeConfigs[i];
                var upgradeIcon = upgradeConfig.UpgradeSprite;
                var nextCost = levels[i] >= upgradeConfig.UpgradeDatas.Length - 1 ? -1 : upgradeConfig.UpgradeDatas[levels[i] + 1].Cost;
                upgradeBarElements[i].SetData(upgradeConfig.UpgradeName,upgradeIcon,levels[i], nextCost);
                upgradeBarElementsDictionary.Add(upgradeConfig.UpgradeType,upgradeBarElements[i]);
            }
            SubscribeToButtons();
        }

        public void UpdateUpgradeBar(UpgradeType type, int level, int nextCost)
        {
            upgradeBarElementsDictionary[type].UpdateData(level, nextCost);
        }
    }
}