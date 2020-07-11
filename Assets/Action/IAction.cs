using UnityEngine;

namespace Action
{
    public interface IAction
    {
        string GetDescription();
        Drone.Drone TakeAction(Drone.Drone drone);
        Color GetBackgroundColor();
    }
}