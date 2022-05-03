using System;
using UnityEngine;

namespace Common.Scripts.ScriptableObjects.UpgradeConfigs
{
    [CreateAssetMenu(fileName = "New Upgrade Config", menuName = "Configs/Upgrades/Upgrade Config", order = 0)]
    public class UpgradeConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite UpgradeSprite { get; private set; }
        [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
        [field: SerializeField] public string UpgradeName { get; private set; }
        
        [field: SerializeField] public UpgradeData DefaultUpgrade { get; private set;}
        [field: SerializeField] public UpgradeData[] UpgradeDatas { get; private set; }
    }

    [Serializable]
    public class UpgradeData
    {
        public int Cost;
        public float Value;
    }

}