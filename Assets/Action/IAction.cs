using Drone;
using UnityEngine;

namespace Action
{
    public interface IAction
    {
        string GetDescription();
        void TakeAction(IDrone drone);
        Color GetBackgroundColor();
    }
}