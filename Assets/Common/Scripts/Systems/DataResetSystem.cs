using Kuhpik;

namespace Common.Scripts.Systems
{
    public class DataResetSystem : GameSystem
    {
        public override void OnUpdate()
        {
            game.InteractNextFrame = false;
            game.RunNextFrame = false;
        }
    }
}