using Common.Scripts.Components;
using Kuhpik;

namespace Common.Scripts.Systems.Loading
{
    public class LoadingSystem : GameSystem
    {
        public override void OnInit()
        {
            game.PlayerEntity = FindObjectOfType<PlayerEntityComponent>();
        }
    }
}