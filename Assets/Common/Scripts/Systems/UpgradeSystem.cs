using Common.Scripts.Components;
using Common.Scripts.ScriptableObjects;
using Common.Scripts.ScriptableObjects.UpgradeConfigs;
using Common.Scripts.UI;
using Kuhpik;
using UnityEngine;

namespace Common.Scripts.Systems
{
    public class UpgradeSystem : GameSystemWithScreen<UpgradeScreen>
    {
        private UpgradeSystemConfig systemConfig;

        public override void OnInit()
        {
            player.CurrentSpeedUpgradeData ??= systemConfig.SpeedUpgradeConfig.DefaultUpgrade;
            player.CurrentTurnUpgradeData ??= systemConfig.TurnUpgradeConfig.DefaultUpgrade;
            player.CurrentRewardUpgradeData ??= systemConfig.RewardUpgradeConfig.DefaultUpgrade;

            var upgradeConfigs = new []{systemConfig.SpeedUpgradeConfig,systemConfig.TurnUpgradeConfig,systemConfig.RewardUpgradeConfig};
            screen.SetupScreen(upgradeConfigs, new []{player.CurrentSpeedLevel,player.CurrentTurnLevel,player.CurrentRewardLevel});
            screen.OnTryToUpgrade += TryToUpgrade;
        }

        public override void OnStateEnter()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered += OnPlayerEntered;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited += OnPlayerExited;
        }
        public override void OnStateExit()
        {
            game.PlayerEntity.InteractionTrigger.OnTriggerEntered -= OnPlayerEntered;
            game.PlayerEntity.InteractionTrigger.OnTriggerExited -= OnPlayerExited;
        }
        private void OnPlayerEntered(GameObject obj)
        {
            if(!obj.TryGetComponent(out UpgradeZoneComponent upgradeZoneComponent)) return;

            screen.Open();
        }
        private void OnPlayerExited(GameObject obj)
        {
            if(!obj.TryGetComponent(out UpgradeZoneComponent upgradeZoneComponent)) return;

            screen.Close();
        }
        
        private void TryToUpgrade(UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeType.Speed:
                    {
                        if(player.CurrentSpeedLevel >= systemConfig.SpeedUpgradeConfig.UpgradeDatas.Length-1) return;

                        var nextLevelData = systemConfig.SpeedUpgradeConfig.UpgradeDatas[player.CurrentSpeedLevel + 1];
                        
                        if(player.Money < nextLevelData.Cost) return;

                        player.Money -= nextLevelData.Cost;
                        player.CurrentSpeedLevel++;
                        player.CurrentSpeedUpgradeData = nextLevelData;

                        var isMaxLevel = player.CurrentSpeedLevel >= systemConfig.SpeedUpgradeConfig.UpgradeDatas.Length - 1;
                        
                        screen.UpdateUpgradeBar(UpgradeType.Speed, player.CurrentSpeedLevel,
                            isMaxLevel ? -1 : systemConfig.SpeedUpgradeConfig.UpgradeDatas[player.CurrentSpeedLevel + 1].Cost);
                        
                        break;
                    }
                case UpgradeType.Turn:
                    {
                        if(player.CurrentTurnLevel >= systemConfig.TurnUpgradeConfig.UpgradeDatas.Length-1) return;

                        var nextLevelData = systemConfig.TurnUpgradeConfig.UpgradeDatas[player.CurrentTurnLevel + 1];
                        
                        if(player.Money < nextLevelData.Cost) return;

                        player.Money -= nextLevelData.Cost;
                        player.CurrentTurnLevel++;
                        player.CurrentTurnUpgradeData = nextLevelData;
                        
                        var isMaxLevel = player.CurrentTurnLevel >= systemConfig.TurnUpgradeConfig.UpgradeDatas.Length - 1;
                        
                        screen.UpdateUpgradeBar(UpgradeType.Turn, player.CurrentTurnLevel,
                            isMaxLevel ? -1 : systemConfig.TurnUpgradeConfig.UpgradeDatas[player.CurrentTurnLevel + 1].Cost);

                        break;
                    }
                case UpgradeType.Reward:
                    {
                        if(player.CurrentRewardLevel >= systemConfig.RewardUpgradeConfig.UpgradeDatas.Length-1) return;

                        var nextLevelData = systemConfig.RewardUpgradeConfig.UpgradeDatas[player.CurrentRewardLevel + 1];
                        
                        if(player.Money < nextLevelData.Cost) return;

                        player.Money -= nextLevelData.Cost;
                        player.CurrentRewardLevel++;
                        player.CurrentRewardUpgradeData = nextLevelData;
                        
                        var isMaxLevel = player.CurrentRewardLevel >= systemConfig.RewardUpgradeConfig.UpgradeDatas.Length - 1;
                        
                        screen.UpdateUpgradeBar(UpgradeType.Reward, player.CurrentRewardLevel,
                            isMaxLevel ? -1 : systemConfig.RewardUpgradeConfig.UpgradeDatas[player.CurrentRewardLevel + 1].Cost);

                        break;
                    }
            }
        }
    }
}