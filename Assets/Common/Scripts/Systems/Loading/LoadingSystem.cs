using Common.Scripts.Components;
using Common.Scripts.Enums;
using Kuhpik;

namespace Common.Scripts.Systems.Loading
{
    public class LoadingSystem : GameSystem
    {
        public override void OnInit()
        {
            game.PlayerEntity = FindObjectOfType<PlayerEntityComponent>();
            FillDictionary();
        }

        private void FillDictionary()
        {
            //TODO somehow automize it
            game.StackItems[ItemType.Default] = 0;
            game.StackItems[ItemType.Pizza] = 0;
        }
    }
}