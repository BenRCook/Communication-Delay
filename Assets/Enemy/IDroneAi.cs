using Action;
using UnityEngine;

namespace Enemy
{
    public interface IDroneAi
    {
        IAction ChooseAction(Drone.AbsDrone drone);
    }
}