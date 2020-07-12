using Drone;
using UnityEngine;

namespace Enemy
{
    public abstract class AbsAiDrone : AbsDrone
    {
        [SerializeField] protected IDroneAi droneAi;

        public override void TakeTurn()
        {
            var action = droneAi.ChooseAction(this);
            PushAction(action);
            TakeNextAction();
        }
    }
}