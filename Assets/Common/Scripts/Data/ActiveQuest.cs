using Common.Scripts.Components;
using Common.Scripts.ScriptableObjects;

namespace Common.Scripts.Data
{
    public class ActiveQuest
    {
        public float GivenTime;
        public float RemainingTime;
        public DeliveryPointComponent Target;
        public DeliveryRequirement Requirement;
        public float Reward;

        public ActiveQuest(float givenTime, float remainingTime, DeliveryPointComponent target, DeliveryRequirement requirement, float reward)
        {
            GivenTime = givenTime;
            RemainingTime = remainingTime;
            Target = target;
            Reward = reward;
            Requirement = requirement;
        }
        
    }
}